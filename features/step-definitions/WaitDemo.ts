import { Given, When, Then } from '@wdio/cucumber-framework'


Given(/^I open the browser and navigate to practiceSite (.+)$/, async function (url ) {

  /* PageLoad TimeOut 
  Unless stated otherwise, it is 300,000 milliseconds. */
  //await browser.setTimeout({ 'pageLoad': 120000 }) 

  /* Implicit Wait -applied in findElement ($)  and findElements ($$)
  Unless stated otherwise, it is 0 milliseconds. */

  //await browser.setTimeout({ 'implicit': 30000 })

  await browser.url(url)
  await browser.maximizeWindow()
  });


  Then(/^I should see a PageHeading with text (.+)$/, async function (tabtext) {
   
    const PageHeader= await $("//h2[text()='Practice Form Controls']")
    expect(await PageHeader).toHaveText(tabtext)
    const value= await PageHeader.getText();
    console.log(value)
  });

  Then(/^validating different Waits$/, async function () {
    const HomeBUtton=await $("//span[text()='HOME']/parent::a")
    const HiddenEle = await $("//div[@id='colorbox']/div[2]")
    const InputMail = await $("form p input[name='email']");
    const tutorialTab =await $("//a[contains(@href,'basic-tutorial')]");    
    
    
    //wait browser untill clickable 
    await browser.waitUntil(async ()=> await (await HomeBUtton).isClickable(),{timeout:5000})

    //  console.log("Href AttributeVale  :  "+  await(HomeBUtton).getAttribute("href"))
    //  console.log("Get CSS value"+ await (await(InputMail).getCSSProperty('width')).value)
    //  console.log("Get CSS value"+ await (await(tutorialTab).getCSSProperty('cursor')).value)    

    //checks fro exists ,visible , center not overlapped , not disabled  , within viewport 
    console.log("Is HomeButton Clickable : "+ await HomeBUtton.isClickable())

    //ISDisplayed 
    console.log("Is HomeBUtton Displayed "+ await HomeBUtton.isDisplayed())

    console.log("Is Hidden Ele displayed "+  await HiddenEle.isDisplayed())

    //Enabled
    console.log("IsEnabled " + await InputMail.isEnabled())
    
    await browser.url("https://the-internet.herokuapp.com/dynamic_loading/1")
    
    //Static wait - Halts execution for given milliseconds
    await browser.pause(3000)     

    const startButton= await $('div#start button')    

    const loadingIcon =await $('div#loading')

    const helloText =await $('div#finish h4')




    //Dynamic Wait 
    //Default Wait time is given in waitforTimeout in wdio.conf.ts (global timeout for all waitFor* commands)
    //Wait for Clickable     
    await (await startButton).waitForClickable({timeout:5000}) //overrides default waitforTimeout
    await (await startButton).click()

    //wait fro displayed and not displayed (disappear)
    await (await loadingIcon).waitForDisplayed({timeout:1000,timeoutMsg:"Loading Icon is not displyed"})    
    await (await loadingIcon).waitForDisplayed({reverse:true , timeout:10000})

    await (await helloText).waitForDisplayed({timeout:10000})
    await browser.waitUntil(async ()=> await(helloText).getText()=='Hello World!' , {
     timeout:5000,
     timeoutMsg:"HelloText is not displayed",
     interval:1000     
    })
    await expect(helloText).toBeDisplayed()
    console.log("Text : "+ await(helloText).getText())

    //WaitFor Exist (checks only for the presence of and not the visibility) and WaitForEnabled 
    browser.url("https://the-internet.herokuapp.com/dynamic_controls");

    const inputEnable= await $('form#input-example input')
    const enableButton=await $('form#input-example button')
    const disableBUtton=await $("//button[text()='Disable']")

    await (enableButton).waitForExist({timeout:5000,timeoutMsg:"Enabled Button is not Exist "})
    await (enableButton).click()

    await (inputEnable).waitForEnabled({timeout:6000 , timeoutMsg:"Input field is not enabled"})
    
    await (disableBUtton).click()
    await browser.waitUntil(async ()=> await(enableButton).getText()=='Enable' , {
      timeout:10000,
      timeoutMsg:"Enable is not displayed",
      interval:2000     
     })
    //await (inputEnable).waitForEnabled({timeout:5000 , timeoutMsg:"Input field is not enabled"})
    await enableButton.click()
    await browser.waitUntil(async ()=> await(disableBUtton).getText()=='Disable' , {
      timeout:10000,
      timeoutMsg:"Disbale is not displayed",
      interval:2000     
     })
    inputEnable.addValue('SampleText')
    await browser.pause(3000)

    
  });

