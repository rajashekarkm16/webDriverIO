import { Given, And, When, Then } from '@wdio/cucumber-framework'

  Given(/^I open the browser and navigate to trsite (.+)$/, async function (trurl) {
    await browser.url(trurl)
    await browser.maximizeWindow()
  });

  Then(/^I should see a holidaytab with text (.+)$/, async function (tabtext) {
    
    const HolidayTab= await $("//span[text()='HOLIDAYS']")

    expect(await HolidayTab).toHaveText(tabtext)
    const value= await HolidayTab.getText();
    console.log(value)
    console.log("*******")
  });


  