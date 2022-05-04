describe("expect number assertion", () =>{
    it('should check for number assertion', async()=>{
        await browser.url('https://phptravels.com/order');
        await browser.maximizeWindow();
        const startupCost = await $('//div[text()="For startups & or new ventures"]//parent::div//span[@class="money number"]');
        const costFreq = await $('//div[text()="For startups & or new ventures"]//parent::div//span[@class="renewal-period"]');
        await startupCost.waitForDisplayed(3000);
        console.log("**** "+parseInt(await startupCost.getText()));
        await expect(parseInt(await startupCost.getText())).toBeLessThan(600);
        await expect(parseInt(await startupCost.getText())).toBeLessThanOrEqual(499);
        await expect(parseInt(await startupCost.getText())).toBeGreaterThan(400);
        await expect(parseInt(await startupCost.getText())).toBeGreaterThanOrEqual(499);
        await expect(parseInt(await startupCost.getText())).toStrictEqual(499);
        //await expect(startupCost).toStrictEqual(499);
        //await expect(parseInt(await startupCost.getText())).toHaveText('499', {asString:true});
        //await expect(" /onetime ").toHaveText('/onetime');
        //await expect(startupCost).toStrictEqual(4965);
    })
})