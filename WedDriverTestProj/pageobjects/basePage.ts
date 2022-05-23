export default class BasePage {

    //#region  [Getter/Setters]

    //#region [Common elelement Property)]

    private get acceptCookiesButton() { return $("//button/span[text()='Accept and Continue']"); }
    private get modalCloseIcon() { return $("//*[contains(@class,'CrossIcon')]"); }

    //#endregion

    //#endregion

    //region [Common Page level Method]

    public async open(path: string) {
        switch (path.toLocaleLowerCase()) {
            case "signup":
                await browser.url('/?accountScreen=SignUp')
            default:
                await browser.url('/')
        }
        await browser.maximizeWindow();
    }

    async waitForPageLoad() {
        await browser.waitUntil(
            async () => await browser.execute(() => document.readyState === 'complete'));
    }

    async acceptCookies() {
        try {
            await (await this.acceptCookiesButton).click();
        }
        catch (error) { }
    }

    async jsClick(element) {
        await browser.execute("arguments[0].click();", await element);
    }

    async jsClear(element) {
        await browser.execute("arguments[0].value='';", await element);
    }

    async getActiveElelemnt()
    {
        return await browser.getActiveElement();
    }

    async closeModal() {
        var isModalOpen = await (await this.modalCloseIcon).isDisplayed();
            if (isModalOpen) {
                await (await this.modalCloseIcon).click();
            }
    }
    //#endregion [ common Page level Method]
}
