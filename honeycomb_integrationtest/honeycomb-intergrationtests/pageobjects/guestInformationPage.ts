import { GuestInformationMessages,PaymentOptions } from '../utilities/constants'
import Page from '../pageobjects/basePage'
import { executionConfig } from '../configs/execution.config'
import { envDetails } from '../configs/environments.config'
export class GuestInformation extends Page {

    //#region [Lead Guest]
    private async title(guestIndex) { return await $("//select[@id='title-field-"+guestIndex+"-input']"); }
    private async firstName(guestIndex) { return await $("//input[@id='first-name-field-"+guestIndex+"-input']"); }
    private async surName(guestIndex) { return await $("//input[@id='last-name-field-"+guestIndex+"-input']"); }
    private get exclamationMark() { return $("//*[@class='css-u3sa8-SvgIcon'][@visibility='visible']"); }

    //#region [Hotel Information Locators]  
    private get hotelInformationTitle() { return $("//h2[text()='Hotel Information']"); }
    private get readHotelInformationinFull() { return $("//span[text()='Read hotel information in full']"); }
    private get hotelInformationHeading() { return $("//h2[@id='full-screen-hotel-information']"); }
    private get hotelInformationcloseIcon() { return $("//button[@type='button'][@class='css-72pt59-IconButton']"); }
    private get hotelInformationText() { return $("//p[@class='css-iwowwx-Body']"); }
    //#endregion[Hotel Information Locators]

    //#region [Payment Locators] 
    private get paymentHeading() {return $("h3#payment-type-group-label");}
    private get paymentCheckboxes() {return $$("//h3[@id='payment-type-group-label']/../ul/li//input");}
    private get paymentOptions() {return $$("//h3[@id='payment-type-group-label']/../ul/li//div[contains(@class,'Body')]");}
    //#endregion[Payment Locators]

    //#region [EmailMe Locators]
    private get emailMeHeading() { return $("//h3[contains(text(),'Email Me')]"); }
    private get emailMeCheckBox() {return $("//h3[contains(text(),'Email Me')]/parent::div//input[@type='checkbox']");}
    private get emailMeInput() {return $("//h3[contains(text(),'Email Me')]/parent::div//input[@type='text']");}
    private get emailMeMessage() {return $("//h3[contains(text(),'Email Me')]/parent::div//p/parent::div");}
    private get emailMeSendButton() {return $("//h3[contains(text(),'Email Me')]/parent::div//button");}
    private get emailMeLoader() {return $("//h3[contains(text(),'Email Me')]/parent::div//button/div");}
    private get sendMeDealMessage(){ return $("//h3[contains(text(),'Email Me')]/parent::div//label/div[2]");}
    //#endregion[EmailMe Locators]

    private get paymentTermsAndConditions() {return $("//div[@bordercolor='highlight.lightest']/p");}
    private get bookingConditions(){return $("//a[text()='booking conditions']");}
    private get agencyTermsOfBussiness(){return $("//a[text()='agency terms of business.']");}
    private get CovidCustomeWaiver(){return $("//a[text()='Covid-19 Customer Waiver']");}
    private get AddMyPaymentDetails(){return $("//span[text()='Add My Payment Details']/parent::button");}


    async NavigateToGuestInformationPage(tripId) {
        if (executionConfig.domain === "hc") {
            var appUrl = envDetails['hc'];
            this.goToUrl(appUrl + "booking/passengers?tripId=" + tripId + "");           
        }
        else if (executionConfig.domain === "tr") {
            var appUrl = envDetails['tr'];
            this.goToUrl(appUrl + "booking/passengers?tripId=" + tripId + "");        
        }
        else if (executionConfig.domain === "sm") {
            var appUrl = envDetails['sm'];
            this.goToUrl(appUrl + "booking/passengers?tripId=" + tripId + "");            
        }
        else {
            throw new Error("Invalid Domain");
        }
    }

    


    //#region [Payment Methods] 
    async verifyPaymentHeading(paymentHeading:string)
    {
        await (await this.paymentHeading).waitForDisplayed({ timeoutMsg: "Payment Heading not displayed" })
        await (await this.paymentHeading).scrollIntoView();
        expect(await this.paymentHeading).toHaveText(paymentHeading,{ message:"Payment Heading Text not Matched" })
    }

    async verifyPaymentOptionsDisplayed()
    {
        for (let paymentOption = 0; paymentOption < await this.paymentOptions.length; paymentOption++) {
            expect(await PaymentOptions.options).toContain(await this.paymentOptions[paymentOption].getText())
        }
    }

    async verifyPaymentTermsAndConditionsNotDisplayed()
    {
        expect(await this.paymentTermsAndConditions).not.toBeDisplayed({ message: "Payment Terms and Conditions is displayed" });
    }

    async verifyPaymentTermsAndConditionsDisplayed()
    {
        expect(await this.paymentTermsAndConditions).toBeDisplayed({ message: "Payment Terms and Conditions is not displayed" });
    }

    
    
    
    //#endregion[Payment Methods]


    //#region [EmailMe Methods]
    async enterEmail(emailAddress)
    {
        await (await this.emailMeHeading).scrollIntoView();
        await this.jsClear(await this.emailMeInput);
        await this.emailMeInput.setValue(emailAddress);
    }

    async validateEmailMeErroMessage()
    {
        await (await this.emailMeHeading).scrollIntoView();
        expect(await this.emailMeMessage).toHaveText(GuestInformationMessages.EmailMeInvalidErrorMsg,{ message:"Email me Error Text not Matched" })
    }

    async validateEmailMeSuccessMessage(emailId:string)
    {
        console.log("Email Message : "+ await (await this.emailMeMessage).getText())
        expect(await this.emailMeMessage).toHaveText(GuestInformationMessages.EmailMeSuccessMsg +emailId,{ message:"Email me Success Text not Matched" })
    }

    async clickSendBestDeals()
    {
        await (await this.emailMeHeading).scrollIntoView();        
        await (await this.emailMeCheckBox).click() 
    }

    async VerifySendMeBestDealsChecked()
    {
        expect(await this.emailMeCheckBox).toBeChecked({message:" Send me best deals is not checked "})
    }

    async verifyEmailMeHeadingText()
    {
        expect(await this.emailMeHeading).toHaveText("Email Me This Hotel",{ message:"Email Heading Text not Matched" })
    }

    async verifySendMeDealsText()
    {
        expect(await this.sendMeDealMessage).toHaveText(GuestInformationMessages.SendMeDealsMessage,{ message:"send me Deals Text not Matched" })
    }

    async sendEmailMeQuote()
    {
        await (await this.emailMeSendButton).waitForDisplayed({ timeoutMsg: "Email me send button not displayed" })
        await (await this.emailMeHeading).scrollIntoView();
        await (await this.emailMeSendButton).click();
    }

    async waitForEmailMeToSend(): Promise<void> {
        try {
            await (await this.emailMeLoader).waitForExist({ timeout: 1000 });
            await (await this.emailMeLoader).waitForExist({ timeout: 20000, reverse: true });
        }
        catch (error) { }
    }    
    //#endregion[EmailMe Methods]

    //#region [Hotel Information Methods] 
    async VerifyHotelInformationTitleDisplayed() {
        await (await this.hotelInformationTitle).isDisplayed();
    }

    async clickOnHotelInformationLink() {
        if (await this.readHotelInformationinFull.isDisplayed())
            await this.readHotelInformationinFull.click();
    }
    async VerifyHotelInformationHeadingDisplayed() {
        await (await this.hotelInformationHeading).isDisplayed();
    }

    async VerifyHotelInformationTextDisplayed() {
        await (await this.hotelInformationText).isDisplayed();
    }

    async VerifyReadHotelInformationLinkDispalyed(expectedLinkText) {
        var actualLinkText = await (await this.readHotelInformationinFull).getText();
        expect(expectedLinkText).toEqual(actualLinkText);
    }

    async clickOnCloseIcon() {
        await (await this.hotelInformationcloseIcon).click();
    }
    //#endregion[Hotel Information Methods]
}

export default new GuestInformation();