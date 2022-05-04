describe("expect assertion methods", () =>{
    it ('should check for the fields to be displayed, exist, exist, focused, clickable, enabled, selected, checked' , async () =>{
        await browser.url('https://the-internet.herokuapp.com/dynamic_controls');
        await browser.maximizeWindow();
        const checkbox = await $('//div[@id="checkbox"]/input');
        const removeButton = await $('//form[@id="checkbox-example"]/button');
        const textBox = await $('//form[@id="input-example"]/input');
        const enableButton = await $('//form[@id="input-example"]/button');
        //toBeDisplayed - checks if element is displayed on web page
        await expect(checkbox).toBeDisplayed({wait:3000, interval:100, message:'checkbox not visible'});
        //toExist - checks if element exist or not in DOM. same for toBePresent, toBeExisting
        await expect(textBox).toExist();
        
        //toBeClickable - check if an elemnent can be clicked
        await expect(removeButton).toBeClickable();
        //toBeDisabled - to check if an element is disabled or not.
        await expect(textBox).toBeDisabled();
        //or
        await expect(textBox).not.toBeEnabled();
        await enableButton.click();
        //toBeEnabled- to check if enabled or not.
        await expect(textBox).toBeEnabled();
        //or
        await expect(textBox).not.toBeDisabled();
        await textBox.click();
        //toBeSelected - returns true or false if element is selected or not
        //await expect(textBox).toBeSelected();
        //await expect(checkbox).toBeSelected();
        //await expect(checkbox).toBeChecked();
        //toBeFocused- checks if element has focus. works only in web context.
        await expect(textBox).toBeFocused();

        await checkbox.click();
        //await expect(textBox).toBeSelected();
        await expect(checkbox).toBeSelected();
        await expect(checkbox).toBeChecked();
    }
    )
    it ('should check for assertions like atrribute, attr value, element prperty' , async () =>{
        const checkbox = await $('//div[@id="checkbox"]/input');
        const removeButton = await $('//form[@id="checkbox-example"]/button');
        const textBox = await $('//form[@id="input-example"]/input');
        const enableButton = await $('//form[@id="input-example"]/button');
        const contentDiv= await $('//div[@id="content"]');
        const loading = await $('#loading');
        //toHaveAttribute - checks if element has certain attr with spcific value. same for toHaveAttr
       await expect(enableButton).toHaveAttr('autocomplete', 'off');
       //toHaveAttributeContaining - checks if element has certain attr that contains value. same for toHaveAttrContaining
       await expect(enableButton).toHaveAttrContaining('onclick', 'swap');

       console.log("*****" + await contentDiv.getAttribute("class"));
       //toHaveElementClass - checks if element has a certain class name
       await expect(contentDiv).toHaveElementClass("large-12");
       //toHaveElementClassContaining - checks if element has certain class name that contains value
       await expect(contentDiv).toHaveElementClass('columns');
       await expect(contentDiv).toHaveElementClassContaining('large-12 columns');
       const width = await (await (textBox).getCSSProperty('width')).value;
       console.log("*****"+width);
       //toHaveElementPropety - checks if element has cerain property.
       await expect(textBox).toHaveElementProperty('type', 'text');
       
       await textBox.click();
       await textBox.addValue('test expect');
       //toHaveValue - to check the value of input element
       await expect(textBox).toHaveValue('test expect');
       //toHaveValueConatining - to check input element contains value
       await expect(textBox).toHaveValueContaining('expect');




    }
    )


}
)
