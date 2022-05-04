import Page from "./page";

class PageObjectPractice extends Page
{
    get PageHeader() { return $("//h2[text()='Practice Form Controls']")  }
    get HomeButton() { return $("//span[text()='HOME']/parent::a")  }
    get HiddenEle () {return $("//div[@id='colorbox']/div[2]")}
    get InputMail() {return $("form p input[name='email']")}
    get tutorialTab() {return $("//a[contains(@href,'basic-tutorial')]")}


    private get Product_dropdown() {return $("select#first")}
    get Animal_dropdown() {return $("select#animals")}


    getProduct_dropdown()
    {
        return this.Product_dropdown
    }


    async selectDropDown(element:WebdriverIO.ElementArray, value:string)
    {
        for(let i=0; i<element.length; i++)
        {
            const elem=await (element[i].getAttribute(value));
            if(elem== value)
            {
                await (element[i]).click();
                break;
            }              
        }
    }

    async SelectProduct(value:string)
    {
        await (await this.Product_dropdown).selectByVisibleText(value)
    }

    async SelectAnimal(element:WebdriverIO.Element,value:string)
    {
        await (await element).selectByVisibleText(value)
    }


    


}
export default new PageObjectPractice() 