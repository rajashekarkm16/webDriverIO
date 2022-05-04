import LoginPage from  '../pageobjects/login.page';
import securePage from '../pageobjects/secure.page';
import SecurePage from '../pageobjects/secure.page';

describe('My Login application', () => {
    it('should login with valid credentials', async () => {
        await LoginPage.open();
        await browser.maximizeWindow();

        //toHaveUrl- checks if browser is on specific page
        await expect(browser).toHaveUrl("https://the-internet.herokuapp.com/login", {wait:3000, interval:100, message:'wrong url'});
        //toHaveUrlContaining - checks a part of the url
        await expect(browser).toHaveUrl("herokuapp", {containing:true});
        await LoginPage.login('tomsmith', 'SuperSecretPassword!');
        //to ignore case use string option
        await expect(browser).toHaveUrlContaining("SECURE",{ignoreCase:true});
        //toHaveTitle - to check web page title
        await expect(browser).toHaveTitle('The Internet');
        //toHaveTitleContaining - to check part of the web title
        await expect(browser).toHaveTitleContaining('Internet');
        //toBeDispayed - to check if element isdisplayed or not
        
         //same as toExist
        await expect(SecurePage.flashAlert).toBeExisting();
        console.log (await (securePage.flashAlert).getText());
        //use of string options and command options
       await SecurePage.flashAlert.waitForDisplayed({timeout: 5000});
        //const textvalue = await (SecurePage.flashAlert).getText();
        //console.log(textvalue);
        //await expect(SecurePage.flashAlert).toHaveText('You logged into a secure area!\n×', {message:'Text do not match', wait:5000, interval:200});
        //await expect(SecurePage.flashAlert).toHaveTextContaining(
         //   'You logged into a secure');
    });
});


