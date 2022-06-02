import Page from './basePage'
import CommonFunctions from '../utilities/commonFunctions';

export class LandingPage extends Page{
    private get introTextWidgetHeader() { return $("(//h2[contains(text(), 'Why')])[1]")};
    private get introTextWidgetBody() { return $("//div[contains(@class,'Cropper') and contains(@id,'honey')]")};
    private get introTextWidgetMore() { return $("//span[text()='More']")};
    private get introTextWidgetLess() { return $("//span[text()='Less']")};
    private get thingsToDoWidgetHeader() {return $("//h2[text()='Things to do']")};
    private get thingsToDoWidgetBody() {return $("//h2[text()='Things to do']/following-sibling::p")};
    private get thingsToDoTab() {return $("//h2[text()='Things to do']//ancestor::section//div[contains(@class,'tablist')]")};
    private get thingsToDoTabButton() {return $$("//h2[text()='Things to do']//ancestor::section//div[contains(@class,'tablist')]/button")};
    private get thingsToDoImages() {return $$("//h2[text()='Things to do']//ancestor::section//img")};
    private get thingsToDoImagesTextSection(){return $$("//h2[text()='Things to do']//ancestor::section//div[contains(@class,'slick-track')]")}
    private get thingsToDoImagesText() {return $$("//h2[text()='Things to do']//ancestor::section//div[contains(@class,'CardContent')]/div")};
    private get thingsToDoImagesHeader() {return $$("//h2[text()='Things to do']//ancestor::section//div[contains(@class,'CardContent')]/h3")};
    private get thingsToDoLeftArrow() {return $("(//button[contains(@class,'carousel__arrow')]//*[contains(@class,'LeftIcon')])[1]")};
    private get thingsToDoRightArrow() {return $("(//button[contains(@class,'carousel__arrow')]//*[contains(@class,'RightIcon')])[1]")};
    
    private thingsToDoTabRandomIndex = 0;

    async introTextWidgetValidation(){
      await expect(await this.introTextWidgetHeader).toBeDisplayed({message:'intro text widget header is not displayed'}); 
      await expect(await this.introTextWidgetHeader).toHaveText('Why Spain?',{message:'Header do not match'});
      await expect(await this.introTextWidgetBody).toBeDisplayed({message:'intro text widget body text is not displayed'});
      await expect(await this.introTextWidgetBody).not.toHaveText('', {message:'Body text is empty'});
    }

    async verifyMoreLink(){
        await expect(await this.introTextWidgetMore).toBeDisplayed({message:'More link is not displayed'});
        await expect(await this.introTextWidgetMore).toBeEnabled({message:'More link is not enabled'});
    }

    async clickMoreLink(){
        await (await this.introTextWidgetMore).click();
    }

    async verifyIntroTextEnlarged(){
        await expect(await this.introTextWidgetBody).toHaveAttr('aria-expanded', 'true',{message:'Intro text not expanded'});
    }

    async verifyLessLink(){
        await expect(await this.introTextWidgetLess).toBeDisplayed({message:'Less link is not displayed'});
        await expect(await this.introTextWidgetLess).toBeEnabled({message:'Less link is not enabled'});
    }

    async clickLessLink(){
        await (await this.introTextWidgetLess).click();
    }

    async verifyIntroTextCollapsed(){
        await expect(await this.introTextWidgetBody).toHaveAttr('aria-expanded', 'false',{message:'Intro text not expanded'});
    }

    async thingsToDoWidgetValidation(){
        await expect(await this.thingsToDoWidgetHeader).toBeDisplayed({message:'Things to do widget header is not displayed'});
        await expect(await this.thingsToDoWidgetBody).not.toHaveText("",{message:'Body text is empty'});
        await expect(await this.thingsToDoWidgetBody).toBeDisplayed({message:'Body text is not displayed'});
    }

    async thingsToDoWidgetContentValidation(){
        await this.thingsToDoImages[1].scrollIntoView();
        await expect(await this.thingsToDoTab).toBeDisplayed({message:'Things to do widget tab is not displayed'});
        await expect(await this.thingsToDoImages[1]).toBeDisplayed({message:'Things to do widget image is not displayed'});
        await expect(await this.thingsToDoImagesText[1]).toBeDisplayed({message:'Things to do widget image text is not displayed'});
    }
    
    async verifyImageNotClickable(){
        const imageLocation = await (await this.thingsToDoImages[1]).getLocation();
        console.log(imageLocation);
        await this.thingsToDoImages[1].click();
        const imageLocationAfterClick = await (await this.thingsToDoImages[1]).getLocation();
        await expect(imageLocation).toEqual(imageLocationAfterClick);
    }
    
    async verifyTextNotClickable(){
        const textLocation = await (await this.thingsToDoImagesHeader[1]).getLocation();
        await this.thingsToDoImagesHeader[1].click();
        const textLocationAfterClick = await (await this.thingsToDoImagesHeader[1]).getLocation();
        await expect(textLocation).toEqual(textLocationAfterClick);

        const imageTextLocation = await (await this.thingsToDoImagesText[1]).getLocation();
        await this.thingsToDoImagesText[1].click();
        const imageTextLocationAfterClick = await (await this.thingsToDoImagesText[1]).getLocation();
        await expect(imageTextLocation).toEqual(imageTextLocationAfterClick);
    }
    
    private async storeRandomNumber() {return await CommonFunctions.getRandomNumber(1, await this.thingsToDoTabButton.length)}; 
    async selectAnyTab(){
        if (await this.thingsToDoTabRandomIndex==0)
            this.thingsToDoTabRandomIndex = await this.storeRandomNumber();
        
        await (await this.thingsToDoWidgetHeader).scrollIntoView();
        await (await this.thingsToDoTabButton[await this.thingsToDoTabRandomIndex]).waitForDisplayed({timeout:4000});    
        await await this.thingsToDoTabButton[await this.thingsToDoTabRandomIndex].click();
    }

    async verifyTabHighlighted(){
        if (await this.thingsToDoTabRandomIndex==0)
            this.thingsToDoTabRandomIndex = await this.storeRandomNumber();
        await expect(await this.thingsToDoTabButton[this.thingsToDoTabRandomIndex]).toHaveAttr('aria-selected', 'true', {message:'Tab is not selected'});
    }

    async tabImageTextValidation(){
        if (await this.thingsToDoTabRandomIndex==0)
           this.thingsToDoTabRandomIndex = await this.storeRandomNumber();
        await expect(await this.thingsToDoImagesTextSection[await this.thingsToDoTabRandomIndex]).toBeDisplayed({wait:4000, message:'Tab image text section not displayed'});
    }

    async navigateImageRight(){
        await (await this.thingsToDoRightArrow).click();
    }

    async navigateImageleft(){
        await (await this.thingsToDoLeftArrow).click();
    }

}

export default new LandingPage;
