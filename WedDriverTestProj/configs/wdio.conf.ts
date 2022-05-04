import { envDetails } from './environments.config'
import { executionConfig } from './execution.config'
const args = require('minimist')(process.argv.slice(2))
const allureReporter = require('@wdio/allure-reporter').default
const allure = require('allure-commandline');
var fs = require('fs');
export const config: WebdriverIO.Config = {

    //###### COMPILER SETUP ####################################
    autoCompileOpts: {
        autoCompile: true,
        tsNodeOpts: {
            transpileOnly: true,
            project: 'tsconfig.json'
        }
    },
    //###### Spec and Suite File Location ####################################
    specs: [
        './features/**/*.feature'
    ],
    suites: {
        regression: [
            './features/**/*.feature'
        ],
        homePage: [
            './features/searchUnit/*.feature'
        ]
    },
    //###### Execution Excluded Profile ####################################
    exclude: [
        // 'path/to/excluded/files'
    ],

    //###### Execution and Browser Capability Setup ##########################

    services: ['chromedriver'],
    framework: 'cucumber',
    baseUrl: args.baseUrl == undefined ? envDetails[executionConfig.domain.toLocaleLowerCase()] : args.baseUrl,
    maxInstances: executionConfig.maxParallelInstances,
    capabilities: [{
        maxInstances: executionConfig.maxParallelInstances,
        browserName: executionConfig.browser,
        acceptInsecureCerts: true
    }],
    connectionRetryCount: 3,
    reporters: ['spec',
        ['allure', {
            outputDir: '.\\reports\\allure-results',
            disableWebdriverStepsReporting: true,
            disableWebdriverScreenshotsReporting: false,
            useCucumberStepReporter: true,
        }],
        ['junit', {
            outputDir: '.\\reports\\junit',

            outputFileFormat: function (options) { // optional
                return `results.xml`
            }
        }]
    ],

    //###### WebdriverIO Log option ##########################

    // Level of logging verbosity: trace | debug | info | warn | error | silent
    logLevel: 'info',

    //###### bail setUP ##########################

    // If you only want to run your tests until a specific amount of tests have failed use
    // bail (default is 0 - don't bail, run all tests).
    bail: 0,

    //
    // Default timeout for all waitFor* commands.
    waitforTimeout: 10000,
    //
    // Default timeout in milliseconds for request
    // if browser driver or grid doesn't send response
    connectionRetryTimeout: 120000,

    //
    // If you are using Cucumber you need to specify the location of your step definitions.
    cucumberOpts: {
        // <string[]> (file/dir) require files before executing features
        require: ['./step-definitions/*.ts'],
        // <boolean> show full backtrace for errors
        backtrace: false,
        // <string[]> ("extension:module") require files with the given EXTENSION after requiring MODULE (repeatable)
        requireModule: [],
        // <boolean> invoke formatters without executing steps
        dryRun: false,
        // <boolean> abort the run on first failure
        failFast: false,
        // <boolean> hide step definition snippets for pending steps
        snippets: true,
        // <boolean> hide source uris
        source: true,
        // <boolean> fail if there are any undefined or pending steps
        strict: false,
        // <string> (expression) only execute the features or scenarios with tags matching the expression
        tagExpression: args.tagExpression == undefined ? executionConfig.tagExpression : args.tagExpression,
        // <number> timeout for step definitions
        timeout: 180000,
        // <boolean> Enable this config to treat undefined definitions as warnings.
        ignoreUndefinedDefinitions: false
    },

    //
    // =====
    // Hooks


    onPrepare: async function (config, capabilities) {

        //#### Remove Report Folder (allure-results and allure-report)
        try {
            await fs.rmdirSync('./reports/allure-results', { recursive: true });      
        } catch (err) {
        }
        try {
            await fs.rmdirSync('./allure-report', { recursive: true });      
        } catch (err) {
        }
        
        //############ Web and Mob view SetUP ######################

        var tabletName = await args.tabletName == undefined ? executionConfig.tabletName : await args.tabletName
        var mobileDeviceName = await args.mobileDeviceName == undefined ? executionConfig.mobileDeviceName : await args.mobileDeviceName
        var platform = await args.executionPlatform == undefined ? executionConfig.executionPlatform : await args.executionPlatform
        switch (platform.toString().toLocaleLowerCase()) {
            case "mobile":
            case "mob":
                capabilities[0]["goog:chromeOptions"] = { mobileEmulation: { 'deviceName': await mobileDeviceName } };
                capabilities[0]["goog:chromeOptions"]["args"] = ['mobile']
                break;
            case "tab":
            case "tablet":
                capabilities[0]["goog:chromeOptions"] = { mobileEmulation: { 'deviceName': await tabletName, } };
                capabilities[0]["goog:chromeOptions"]["args"] = ['tab']
                break;
            case "desktop":
                break;
        }
        //############ End  ######################
    },

    before: async function (capabilities, specs) {

        //####### Set Execution Global value #################
        var platform ='Desktop';
        var device
        try {
            platform = await capabilities['goog:chromeOptions']['args'][0];
            device = await capabilities['goog:chromeOptions']['mobileEmulation']['deviceName'];
        }
        catch (error) { }
        var browserName = await capabilities["browserName"];
        switch (await platform.toString().toLocaleLowerCase()) {
            case "mobile":
            case "mob":
                global.isMobileView = true;
                global.executionPaltform = 'Mobile Emulator';
                global.deviceName = device;
                break;
            case "tab":
            case "tablet":
                // Add method to fetch mobile view in case of platform is Tab
                global.isMobileView = false;
                global.executionPaltform = 'Tablet Emulator';
                global.deviceName = device;
                break;
            case "desktop":
                global.isMobileView = false;
                global.executionPaltform = 'Desktop';
                global.deviceName = browserName;
                break;

        }

    },

    afterStep: async function (step, scenario, result, context) {
        if (result.error) {
            await browser.takeScreenshot();
        }
    },
    afterScenario: async function (world, result, context) {
        if (!result.error) {
            await browser.takeScreenshot();
        }

        //#### Add environment details on allure report
        await allureReporter.addEnvironment("Application URL", config.baseUrl)
        await allureReporter.addEnvironment("Execution Platform", await global.executionPaltform)
        if (global.executionPaltform === 'Desktop') {
            await allureReporter.addEnvironment("Browser Name", await global.deviceName)
        }
        else
        {
            await allureReporter.addEnvironment("Device Name", await global.deviceName)
        }
        
    },

    onComplete: async function () {

      //##### Generate allure reports using command line
        const reportError = await new Error('Could not generate Allure report')
        const generation = await allure(['generate', './reports/allure-results','--clean'])
        return new Promise<void>((resolve, reject) => {
            const generationTimeout = setTimeout(
                () => reject(reportError),
                5000)

                 generation.on('exit', function (exitCode: number) {
                 clearTimeout(generationTimeout)

                if (exitCode !== 0) {
                    return reject(reportError)
                }
                resolve()
            })
        })
    }

}
