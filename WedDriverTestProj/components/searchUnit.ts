import Page from '../pageobjects/basePage'
import DateTimeUtility from '../utilities/dateTimeUtility';
import { MaxCalendarMonths,Color } from '../utilities/constants'



export class SearchUnit extends Page {

    //#region  [Getter/Setters]

    //#region [Search Tab] 
    private async productTabName(tabName) {
        return await $("//*[@role='tablist']//*[contains(text(),'" + tabName + "')]");
    }

    //#endregion[Search Tab]

    //#region [Destination]    
    private get labelDestination() { return $("//label[text()='Destination']"); }
    private get textboxDestination() { return $('#destination-autocompleter-field'); }
    private get textboxDestinationForMobile() { return $('#destination-autocompleter-field-modal'); }
    private get headerDestinationAutoCompleter() { return $("//*[contains(text(),'Destination or hotel')]"); }
    private get categoryHeaderDestinationAutoCompleter() { return $("p#category-destinations"); }
    private get categoryIconDestinationAutoCompleter() { return $("//*[contains(@class,'MapIcon')]"); }
    private get subcategoryHeaderDestinationAutoCompleter() { return $("p#category-hotels"); }
    private get hotelIconDestinationAutoCompleter() { return $("//*[contains(@class,'BedIcon')]"); }
    private get crossIcon() { return $("//button/*[contains(@class,'CrossIcon')]"); }
    private get listOfResultsDestinationAutoCompleter() { return $$("//ul[contains(@aria-labelledby,'category')]//li"); }
    private async resultDestinationAutoCompleter(searchResult) {
        return await $("//ul[contains(@aria-labelledby,'category')]//li//p[contains(normalize-space(.), '" + searchResult + "')]");
    }
    private get destinationError() { return $("//p[text()='No matches found']"); }

    //#endregion

    //#region [Dates]
    private get dateHeader() {return $("//div[text()='Dates']");}
    private get buttonAddDates() { return $("//div[text()='Dates']/preceding-sibling::button"); }
    private get dateModalHeader() {return $("//div[@role='tooltip']//h5[contains(@class,'Heading')]");}
    private get calenderDefaultMonth() { return $("//div[@class='DayPicker-Months']/div[1]/div[@class='DayPicker-Caption']"); }
    private get defaultSelectedDay() {return $("div.DayPicker-Day--today div div");}
    private get calenderForm() { return $("//div[@class='DayPicker-Months']"); }
    private get previousMonth() { return $("//div[@class='DayPicker-wrapper']//*[contains(@class,'LeftIcon')]/.."); }     
    private get nextMonth() { return $("//div[@class='DayPicker-wrapper']//*[contains(@class,'RightIcon')]/.."); }
    private get monthAndYear() {return $$("//div[@class='DayPicker-Caption']/div");}
    private get footerMessage() {return $("//div[@role='tooltip']/div//div[contains(@class,StyleUtility)]//p");}
    private get resetLink() {return $("//a[contains(text(),'Reset')]");}
    private get confirmDates() {return $("//span[text()='Done']/parent::button");}  
    private get daysSelected() {return $$("div.DayPicker-Day--selected");}      
    private async calenderDay(areaLabelValue) {
        return await $("//div[@aria-label='" + areaLabelValue + "']");

    }
    //#endregion[Dates]

    //#region [Guest Locator]

    private get helpMessage() { return $(".css-1ov7o6f-StyleUtility"); }
    private get roomInformationMessage() { return $("//p[text()='Rooms']/..//div"); }
    private get adultsInformationMessage() { return $("//p[text()='Adults']/..//div"); }
    private get childrenInformationMessage() { return $("//p[text()='Children']/..//div"); }
    private get ageValidation() { return $("//p[text()='Please enter ages of all children']"); }
    private get buttonGuests() { return $("//*[text()='Guests']/..//button"); }
    private get popupRoomandGuest() { return $("//*[text()='Rooms & guests']"); }
    private get buttonDone() { return $("//span[text()='Done']/.."); }
    private get childAgeInputAlert() { return $("//*[contains(text(),'Age of child')]"); }
    private async inputRoomSelection(roomCount) {
        return await $("//div[@class='css-jwy12i-Stacker']//input[@value='" + roomCount + "']");
    }

    private async areaRoomSelection(roomCount) {
        return await $("//div[@class='css-jwy12i-Stacker']//input[@value='" + roomCount + "']/../div");
    }

    private async inputAdultField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//input[@id='adults-picker-" + roomNumber + "']");
    }

    private async inputChildrenField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//input[@id='child-picker-" + roomNumber + "']");
    }

    private async plusIconAdultField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//button[@aria-controls='adults-picker-" + roomNumber + "' and @aria-label='Increase']");
    }
    private async plusIconChildrenField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//button[@aria-controls='child-picker-" + roomNumber + "' and @aria-label='Increase']");
    }

    private async minusIconAdultField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//button[@aria-controls='adults-picker-" + roomNumber + "' and @aria-label='Decrease']");
    }
    private async minusIconChildrenField(roomNumber) {
        roomNumber = Number(roomNumber) - 1;
        return await $("//button[@aria-controls='child-picker-" + roomNumber + "' and @aria-label='Decrease']");
    }

    private async childInput(roomNumber, childNumber) {
        roomNumber = Number(roomNumber) - 1;
        childNumber = Number(childNumber) - 1;
        return await $("//input[@id='room-" + roomNumber + "-child-" + childNumber + "']");
    }

    private async childAgeInput(age) {
        return await $("//input[@name='age-toggle' and @value='" + age + "']");
    }

    private async childAgeInputAlertButton(age) {
        return await $("//input[@name='age-toggle' and @value='" + age + "']/..");
    }

    //#endregion[Guest]


    private get searchButton() { return $("//button/span[text()='Search']"); }


    //#endregion

    //#region  [Methods]

    async clickonSearchButton() {
        await (await this.searchButton).click();
    }

    //#region  [Destination]

    async openDestinationAutoCompleter() {
        if(!await (this.headerDestinationAutoCompleter).isDisplayed())
        await this.textboxDestination.click();
    }

    async populateDestination(destination) {
        await this.openDestinationAutoCompleter();
        if (await global.isMobileView) {
            await this.jsClear(await this.textboxDestinationForMobile);
            await this.textboxDestinationForMobile.setValue(destination);
        }
        else {
            await this.jsClear(await this.textboxDestination);
            await this.textboxDestination.setValue(destination);
        }
    }

    async verifyResultsInDestinationAutoCompleter(searchResult) {
        await (await this.resultDestinationAutoCompleter(searchResult)).waitForDisplayed({ timeout: 30000 })
    }

    async selectDestinationFromAutoCompleter(searchResult) {
        await (await this.resultDestinationAutoCompleter(searchResult)).click();
    }

    async selectDestinationFromAutocompleterUsingKeyborad(searchResult) {
        await (await this.resultDestinationAutoCompleter(searchResult)).waitForDisplayed();
        await (await this.textboxDestination).keys('Keydown');
        await (await this.resultDestinationAutoCompleter(searchResult)).keys('Enter');
    }

    async verifyDestination(searchResult) {
        await expect(await this.textboxDestination).toHaveValueContaining(searchResult, { ignoreCase: true })
    }

    async selectDestination(destination: string) {
        await this.populateDestination(destination);
        await this.verifyResultsInDestinationAutoCompleter(destination);
        await this.selectDestinationFromAutoCompleter(destination);
        await this.verifyDestination(destination);        

    }
    async populateSearch(destination, checkinDaysFromToday, durationOfStay, guestInfo=[{"Adults": 1,"Child": [1]},{"Adults": 2,"Child": [3]}]){
        await this.selectDestination(destination)
        await this.populateDate(checkinDaysFromToday, durationOfStay); 
        await this.selectRoomAndGuests(guestInfo)         
        await this.searchButton.click(); 
    }

    async populateSearchByDestination(destination : string = 'Dubai'){
        await this.selectDestination(destination)
        await this.populateDate();    
        await this.searchButton.click(); 
    }

    async populateDate(checkinDaysFromToday: number =60, durationOfStay: number = 5){
        await this.selectDate(await DateTimeUtility.addOrSubtractDaysToCurrentDate(Number(checkinDaysFromToday)));
        await this.selectDate(await DateTimeUtility.addOrSubtractDaysToCurrentDate(Number(checkinDaysFromToday)+Number(durationOfStay)));
    }

    async verifyDestinationPlaceholder(validationMessage) {
        await expect(await this.textboxDestination).toHaveAttributeContaining("placeholder", validationMessage, { ignoreCase: true })
    }

    async verifyDestinationAutoCompleterHeader(headerText) {
        await expect(await this.headerDestinationAutoCompleter).toHaveTextContaining(headerText, { ignoreCase: true })
    }

    async verifyDestinationAutoCompleterCategoryHeader(headerText) {
        await expect(await this.categoryHeaderDestinationAutoCompleter).toHaveTextContaining(headerText, { ignoreCase: true })
    }

    async verifyDestinationAutoCompleterSubCategoryHeader(headerText) {
        await expect(await this.subcategoryHeaderDestinationAutoCompleter).toHaveTextContaining(headerText, { ignoreCase: true })
    }

    async closePopUp() {
        await this.crossIcon.click();

    }
    async verifyDestinationAutoCompleterClosed() {
        await this.crossIcon.waitForDisplayed({ reverse: true })
    }

    async verifyDestinationAutoCompleterCategoryIcon() {
        await this.categoryIconDestinationAutoCompleter.waitForDisplayed();
    }

    async verifyDestinationAutoCompleterHotelIcon() {
        await this.hotelIconDestinationAutoCompleter.waitForDisplayed();
    }

    async verifyHotelCountInDestinationAutoCompleter() {

        await this.listOfResultsDestinationAutoCompleter.forEach(async(element: any) => {            
             await expect(await element).toHaveTextContaining("hotels", { ignoreCase: true })
        });

    }

    async verifyDestinationErrors() {
        const redColorSeries = Color.error;
        var color = await this.textboxDestination.getCSSProperty('outline-color')
        expect(redColorSeries).toContain(color.value);
        color = await this.textboxDestination.getCSSProperty('color')
        expect(redColorSeries).toContain(color.value);
        color = await this.labelDestination.getCSSProperty('color')
        expect(redColorSeries).toContain(color.value);
    }

    async verifyActiveElementText(activeElelementText) {
        const activeElement = await this.getActiveElelemnt();
        await expect(await  $(activeElement).parentElement()).toHaveTextContaining(activeElelementText);
    }

    async verifyNoMatchesFound() {
        await (await this.destinationError).waitForDisplayed();
        await expect(await this.destinationError).toBeDisplayed();
    }

    //#endregion

    //#region  [Date]

    async openCalenderForm() {
        var calenderFormOpen = await (await this.calenderForm).isDisplayed();
        if (!calenderFormOpen) {
            await this.buttonAddDates.click();
            await (await this.calenderForm).waitForDisplayed();
            return this;
        }
        return true;
    }    

    async verifyResetLinkDisplayed()
    {
        await this.openCalenderForm()
        return await expect(await this.resetLink).toBeDisplayed({message:"Reset Link is not displyed in calendar modal"});         
    }

    async verifyResetLinkNotDisplayed()
    {
        await this.openCalenderForm()
        await expect(await this.resetLink).not.toBeDisplayed({message:"Reset Link is displyed in calendar modal"}); 
    }

    async clickReset()
    {
        await (await this.resetLink).waitForDisplayed({timeoutMsg:"Reset Link is not displyed in calendar modal"})
        await await(this.resetLink).scrollIntoView();
        await (await this.resetLink).click();
    }

    async selectMonthFromCalender(date) {
        var presentDate = await (await this.calenderDefaultMonth).getText();
        presentDate = await DateTimeUtility.getDateFormatFromGivenDate(presentDate, "MMMM yyyy", "DD-MMM-yyyy");
        var monthDifference = await DateTimeUtility.getDateDifference(presentDate, date, "month");
        if (monthDifference > 1) {
            for (let i = 0; i < Math.round(Number(monthDifference) - 1); i++) {
                await (await this.nextMonth).click();
            }
        }
        if (monthDifference < 0) {
            for (let i = 0; i < Math.round(Math.abs(Number(monthDifference))); i++) {
                await (await this.previousMonth).click();
            }
        }
    }

    async selectDate(date) {
        await this.openCalenderForm()
        await this.selectMonthFromCalender(date);
        var dateLevel = await DateTimeUtility.getDateFormatFromGivenDate(date, "DD-MMM-yyyy", "ddd MMM DD yyyy");
        await (await this.calenderDay(dateLevel)).click();        
    }

    async closeCalenderPopUp() {
        await this.openCalenderForm()
        await (await this.crossIcon).waitForDisplayed({timeoutMsg:'close Date Icon is not displyed'})
        await (await this.crossIcon).click();
        await (await this.crossIcon).waitForDisplayed({ reverse: true })
    }

    async verifyCrossIconDisplayed()
    {
        await expect(this.crossIcon).toBeDisplayed({message:"close date modal Icon is not displyed"});
    } 
    async verifyCalendarModalClosed()
    {
        await expect(this.calenderForm).not.toBeDisplayed({message:"Calendar Modal not closed"});
    }

    async verifyCalendarModalOpened()
    {
        await expect(this.calenderForm).toBeDisplayed({message:"Calendar Modal not opened"});
    }

    async verifyDateInDateSection(dateSelection) {
        var datetoverify = await DateTimeUtility.getDateFormatFromGivenDate(dateSelection, "DD-MMM-yyyy", "MMM DD, yyyy");
        await expect(await this.buttonAddDates).toHaveTextContaining(datetoverify, { ignoreCase: true })
    }

    async validateDateSelectedInDateField(checkInDate,checkOutDate)
    {       
        await (await this.buttonAddDates).waitForDisplayed({timeoutMsg:'Date field is not displyed'}) 
        await (await this.buttonAddDates).scrollIntoView()
        var checkinDate=await DateTimeUtility.getDateFormatFromGivenDate(checkInDate,"DD-MMM-yyyy", "MMM D, yyyy")
        var checkoutDate=await DateTimeUtility.getDateFormatFromGivenDate(checkOutDate,"DD-MMM-yyyy", "MMM D, yyyy")        
        var dateValue=checkinDate+" - "+checkoutDate;         
        await expect(await this.buttonAddDates).toHaveText(dateValue)        
    }

    async validateCheckinDateSelectedInDateField(checkInDate)
    {
        await (await this.buttonAddDates).waitForDisplayed({timeoutMsg:'Date field is not displyed'}) 
        await (await this.buttonAddDates).scrollIntoView()
        var checkinDate=await DateTimeUtility.getDateFormatFromGivenDate(checkInDate,"DD-MMM-yyyy", "MMM D, yyyy")
        var dateValue=checkinDate+" -"
        await expect(await this.buttonAddDates).toHaveText(dateValue)
    }

    async verifyCheckiDateHighlightedInCalendarModal(checkInDate)
    {
        var checkinDate=await DateTimeUtility.getDateFormatFromGivenDate(checkInDate,"DD-MMM-yyyy", "ddd MMM DD yyyy")            
        await expect(await this.daysSelected[0]).toHaveAttribute('aria-label', checkinDate)
    } 

    async validateDateSelectedInFooter(checkInDate,checkOutDate)
    {       
        await (await this.footerMessage).scrollIntoView()
        var checkinDate=await DateTimeUtility.getDateFormatFromGivenDate(checkInDate,"DD-MMM-yyyy", "MMM D, yyyy")    
        var checkoutDate=await DateTimeUtility.getDateFormatFromGivenDate(checkOutDate,"DD-MMM-yyyy", "MMM D, yyyy")
        var numberOfNights=await DateTimeUtility.getDateDifference(checkInDate,checkOutDate,"days")     
        var dateValue=checkinDate+" - "+checkoutDate+" ("+ numberOfNights+" nights)"+" Reset";       
        await expect(await this.footerMessage).toHaveText(dateValue)        
    }

    async validateNumberOfNightsInFooter(checkInDate,checkOutDate)
    {
        var numberOfNights=await DateTimeUtility.getDateDifference(checkInDate,checkOutDate,"days")
        await expect(await this.footerMessage).toHaveTextContaining("("+ numberOfNights+" nights)")
    }

    async verifyDatesValidationMessage(validationMessage) {
         await expect(await this.buttonAddDates).toHaveTextContaining(validationMessage, { ignoreCase: true })
    }

    async verifyDateHeader()
    {
        await expect(this.dateHeader).toBeDisplayed({message:"Date header is not displyed"});       
    }

    async verifyDatePlaceholder(placeholder:string)
    {
        await this.verifyDatesValidationMessage(placeholder)
    }

    async verifyDateModalHeader(modalHeader:string)
    {
        await this.openCalenderForm()
        await (await this.dateModalHeader).waitForDisplayed({timeoutMsg:'Date modal is not displyed'})         
        await expect(await this.dateModalHeader).toHaveText(modalHeader, { ignoreCase: true })
    }      

    async verifyCurrentAndNextMonthText()
    {
        await expect(await this.monthAndYear[0]).toHaveText(await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(0,"MMMM yyyy"),{ignoreCase:true})                
        await expect(await this.monthAndYear[1]).toHaveText(await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(1,"MMMM yyyy"),{ignoreCase:true})
    }

    async verifyDefaultSelectedDay()
    {
        await expect(await this.defaultSelectedDay).toHaveText(await DateTimeUtility.addOrSubtractDaysToCurrentDate(0,"D"))
    }
    
    async validateFooterMessage(footerMessage:string)
    {
        await (await this.footerMessage).scrollIntoView()
        await expect(await this.footerMessage).toHaveText(footerMessage)
    }

    async verifyDoneButtonDisplayed()
    {
        await this.openCalenderForm()
        await (await this.confirmDates).waitForDisplayed({timeoutMsg:'Done button is not displyed'})
        await (await this.confirmDates).scrollIntoView()        
        await expect(await this.confirmDates).toBeDisplayed({message:"Date Button is not displyed in calendar modal"})        
    }

    async verifyDoneButtonNotEnabled()
    {
        await expect(this.confirmDates).not.toBeEnabled({message:"Date Button is enabled in calendar modal"}); 

    }

    async verifyDoneButtonEnabled()
    {
        await this.openCalenderForm()
        await (await this.confirmDates).waitForDisplayed({timeoutMsg:'Done button is not displyed'})  
        await (await this.confirmDates).scrollIntoView()
        await expect(await this.confirmDates).toBeEnabled({message:"Date Button is not enabled in calendar modal"})
    }

    async clickDoneonCalendar()
    {
        await this.openCalenderForm()
        await (await this.confirmDates).waitForDisplayed({timeoutMsg:'Done button is not displyed'}) 
        await (await this.confirmDates).scrollIntoView()
        await (await this.confirmDates).click()
    }


    async verifyPreviousIconDisabled()
    {
        await this.openCalenderForm()
        await expect(await this.previousMonth).not.toBeEnabled({message:"Previous Icon is enabled in calendar modal"});
    }   
    
    async verifyNextIconDisabled()
    {
        await this.openCalenderForm()
        await expect(await this.nextMonth).not.toBeEnabled({message:"NextMonth Icon is enabled in calendar modal"});
    } 

    async verifyPreviousIconEnabled()
    {
        await this.openCalenderForm()
        await expect(await this.previousMonth).toBeEnabled({message:"Previous Icon is not enabled in calendar modal"});
    }

    async verifyNextIconEnabled()
    {
        await this.openCalenderForm()
        await expect(await this.nextMonth).toBeEnabled({message:"Next Icon is not enabled in calendar modal"});
    }

    async clickonPreviousIcon()
    {
        await this.openCalenderForm()
        await (await this.previousMonth).waitForDisplayed({timeoutMsg:'Previous month is not displyed'}) 
        await (await this.previousMonth).click()
    }

    async clickonNextIcon()
    {
        await this.openCalenderForm()
        await (await this.nextMonth).waitForDisplayed({timeoutMsg:'Next month is not displyed'})         
        await (await this.nextMonth).click();                
    }

    async verifyIsNextMonthDisplayed()
    {
        await this.openCalenderForm()
        var monthToDisplay=await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(2,"MMMM yyyy")       
        await (await this.monthAndYear[1]).waitForDisplayed({timeoutMsg:'Next month is not displyed'}) 
        await expect(await this.monthAndYear[1]).toHaveText(monthToDisplay,{ignoreCase:true,message:"Next month Text is not Matched "})
    }

    async verifyIsPreviousMonthDisplayed()
    {
        await this.openCalenderForm()
        var monthToDisplay=await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(1,"MMMM yyyy")        
        await (await this.monthAndYear[1]).waitForDisplayed({timeoutMsg:'Next month is not displyed'}) 
        await expect(await this.monthAndYear[1]).toHaveText(monthToDisplay,{ignoreCase:true})
    }

    async verifyNumberOfMonthsTobedisplayedinCalendar()
    {
        await this.openCalenderForm()
        var LastMonthToDisplayInCalendar=await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(MaxCalendarMonths,"MMMM yyyy") 
        for(let counter=1;counter<=MaxCalendarMonths-1;counter++)
        {
            await this.nextMonth.click(); 
        }               
        await expect(await this.monthAndYear[1]).toHaveText(LastMonthToDisplayInCalendar,{ignoreCase:true})  
        await this.verifyNextIconDisabled()             
    }

    async verifyDisableDaysBeforeToday()
    {
        const daySelected : number = Number(await this.defaultSelectedDay.getText())   
        const currentMonthandYear=await DateTimeUtility.addOrSubtractMonthsToCurrentMonth(0,'MMM YYYY')         
        for(let counter=daySelected-1;counter>=1;counter--)
        {                                   
            const datevalue=await DateTimeUtility.getDateFormatFromGivenDate(counter+" " +currentMonthandYear,'DD MMM YYYY','ddd MMM DD yyyy')            
            await expect(await this.calenderDay(datevalue)).toHaveAttribute('aria-disabled', 'true',{message:"Day "+counter + " : is not disabled"})           
        }
    }

    

    //#endregion

    //#region [Guests Method]

    async openRoomsAndGuestsModal() {
        var itemOpenCheck = await (await this.popupRoomandGuest).isDisplayed();
        if (!itemOpenCheck) {
            await this.jsClick(await this.buttonGuests);
            await (await this.popupRoomandGuest).waitForDisplayed({ timeout: 30000 });
            expect(await this.popupRoomandGuest).toBeDisplayed();
        }
    }
    async verifyRoomsAndGuestsModalClosed()
    {
        expect(await this.popupRoomandGuest).not.toBeDisplayed();
    }

    async selectRoom(roomCount :Number) {
        (await this.jsClick(await this.inputRoomSelection(roomCount)))
    }

    async verifyRoomsSection(roomCount: Number) {
        await expect(await this.inputAdultField(roomCount)).toBeDisplayed();
    }

    async selectAdult(roomNumber, AdultCount) {
        var selectAdult = await (await this.inputAdultField(roomNumber)).getValue();
        if (selectAdult === AdultCount) {
            return true;
        }
        var diff = Number(AdultCount) - Number(selectAdult);
        if (diff > 0) {
            for (let i = 0; i < Math.round(Number(diff)); i++) {
                await (await this.plusIconAdultField(roomNumber)).click();
                await this.waitForPageLoad();
            }
        }
        if (diff < 0) {
            for (let i = 0; i < Math.round(Math.abs(Number(diff))); i++) {
                await (await this.minusIconAdultField(roomNumber)).click();
                await this.waitForPageLoad();
            }
        }
    }

    async verifyAdultsCountInRooms(roomNumber:string,adultCount:string)
    {
        var actualAdultsCount = await (await this.inputAdultField(roomNumber)).getValue();
         expect(Number(adultCount)).toEqual(Number(actualAdultsCount));
    }

    async selectChildren(roomNumber, ChildrenCount) {
        var selectedChildren = await (await this.inputChildrenField(roomNumber)).getValue();
        if (selectedChildren === ChildrenCount) {
            return true;
        }
        var diff = Number(ChildrenCount) - Number(selectedChildren);
        if (diff > 0) {
            for (let i = 0; i < Number(diff); i++) {
                await (await this.plusIconChildrenField(roomNumber)).click();
                await this.waitForPageLoad();
            }
        }
        else  {
            for (let i = 0; i < Math.abs(Number(diff)); i++) {
                await (await this.minusIconChildrenField(roomNumber)).click();
                await this.waitForPageLoad();
            }
        }

    }
    async verifyChildrenCountRoomWise(roomNumber,childrenCount)
    {
        var selectedChildren = await (await this.inputChildrenField(await roomNumber)).getValue();
        expect(Number(await childrenCount)).toEqual(Number(await selectedChildren));
    }
    async selectChildAge(roomNumber, childNumber, Age) {

        var selectedChildAge = await (await this.childInput(await roomNumber, await childNumber)).getValue();
        if (Number(selectedChildAge) === Number(await Age)) {
            return true;
        }
        await (await this.childInput(await roomNumber, await childNumber)).click();
        await (await this.childAgeInputAlert).waitForDisplayed();
        await (await this.childAgeInputAlertButton(await Age)).click();
        await this.waitForPageLoad();
        
    }

    async verifyChildrenAgeRoomWise(roomNumber,childNumber, childAge)
    {
        var selectedChildAge = await (await this.childInput(await roomNumber, await childNumber)).getValue();
        expect(Number(childAge)).toEqual(Number(selectedChildAge));
    }

    async clickDoneButton() {
        await (await this.buttonDone).click();
        await (await this.popupRoomandGuest).waitForDisplayed({ reverse: true });
    }

    async selectRoomAndGuests(gestInfo) {
        gestInfo = JSON.parse(gestInfo);
        await this.openRoomsAndGuestsModal();
        var totalRooms = await Number(gestInfo.length);
        var childInfo;
        var totalAdult = 0;
        var totalChild = 0;
        await this.selectRoom(totalRooms);
        await this.verifyRoomsSection(totalRooms)
        for (let i = 1; i <= totalRooms; i++) {
            var AdulutCountRoomWise = await gestInfo[Number(i) - 1]["Adults"];
            totalAdult = Number(totalAdult) + Number(AdulutCountRoomWise);
            await this.selectAdult(i, AdulutCountRoomWise);
            await this.verifyAdultsCountInRooms(i.toString(),AdulutCountRoomWise);
            //@ total child count
            childInfo = await gestInfo[Number(i) - 1]["Child"];
            try {
                totalChild = Number(totalChild) + Number(childInfo.length);
                await this.selectChildren(i, childInfo.length);
                await this.verifyChildrenCountRoomWise(i,childInfo.length);
                for (let j = 1; j <= childInfo.length; j++) {
                    await this.selectChildAge(i, j, childInfo[Number(j - 1)]);
                    await this.verifyChildrenAgeRoomWise(i,j,childInfo[Number(j - 1)]);
                }
            }
            catch (error) { }

        }
        await this.clickDoneButton();
        var adultText = '';
        var childText = '';
        var roomText = '';
        switch (Number(totalAdult)) {
            case 1:
                adultText = '1 Adult';
                break;
            default:
                adultText = totalAdult + ' Adults';
                break;
        }
        switch (Number(totalChild)) {
            case 0:
                childText = '';
            case 1:
                childText = '1 Child';
                break;
            default:
                childText = totalChild + ' Children';
                break;
        }
        switch (Number(totalRooms)) {
            case 1:
                roomText = '1 Room';
                break;
            default:
                roomText = totalRooms + ' Rooms';
                break;
        }
        var defaultText = adultText + ', ' + childText + ", " + roomText;
        if (totalChild === 0) {
            defaultText = adultText + ", " + roomText;
        }
        await this.verifyGuests(defaultText);

    }

    async verifyGuests(guestsText: string)
    {
        await expect(await this.buttonGuests).toHaveTextContaining(guestsText, { ignoreCase: true })
    }

    async verifyColorForRoom(roomNumber :string,colorName:string)
    {
        const blueColorSeries = Color.highlited;
        const greyColorSeries = Color.unselected;
        const color = await ( await this.areaRoomSelection(roomNumber)).getCSSProperty('background-color')
        
        switch(colorName.toLocaleLowerCase().trim())
        {
            case "blue":
            case "highlighted":
                expect(blueColorSeries).toContain(color.value);
                break;
                case"grey":
                expect(greyColorSeries).toContain(color.value);
                break;
                default:
                    expect(false).toBe("feature file has wrong color name")
        }
    }

    async disabledCheckForIncreaseIconForAdults(roomNumber)
    {
        await expect(await this.plusIconAdultField(roomNumber)).toBeDisabled();
    }

    async disabledCheckForDecreaseIconForAdults(roomNumber)
    {
        await expect(await this.minusIconAdultField(roomNumber)).toBeDisabled();
    }

    async enabledCheckForIncreaseIconForAdults(roomNumber)
    {
        await expect(await this.plusIconAdultField(roomNumber)).toBeEnabled()
    }

    async enabledCheckForDecreaseIconForAdults(roomNumber)
    {
        await expect(await this.minusIconAdultField(roomNumber)).toBeEnabled();
    }

    async disabledCheckForIncreaseIconForChildren(roomNumber)
    {
        await expect(await this.plusIconChildrenField(roomNumber)).toBeDisabled();
    }

    async disabledCheckForDecreaseIconForChildren(roomNumber)
    {
        await expect(await this.minusIconChildrenField(roomNumber)).toBeDisabled();
    }

    async verifyHelpMessage(helpText)
    {
        await expect(await this.helpMessage).toHaveTextContaining(helpText,{ ignoreCase: true })
    }

    async verifyRoomsInformation(roomInformation:string)
    {
        await expect(await this.roomInformationMessage).toHaveTextContaining(await roomInformation,{ ignoreCase: true })
    }

    async verifyAdultsInformation(adultsInformation)
    {
        await expect(await this.adultsInformationMessage).toHaveTextContaining(adultsInformation,{ ignoreCase: true })
    }

    async verifyChildrenInformation(childrenInformation)
    {
        await expect(await this.childrenInformationMessage).toHaveTextContaining(childrenInformation,{ ignoreCase: true })
    }

    async verifyAgeValidationMessage()
    {
        await expect(await this.ageValidation).toBeDisplayed();
    }

    async verifyAgeValidationMessageInRedColor()
    {
        const redColorSeries = Color.error;
        var color  = await this.ageValidation.getCSSProperty('color')
        expect(redColorSeries).toContain(await color.value);
    }

    async verifyChildAgeBoxInRedColor(roomNumber,childNumber)
    {
        const redColorSeries = Color.error;
        var color  = await (await this.childInput(roomNumber,childNumber)).getCSSProperty('border-bottom-color')
        expect(redColorSeries).toContain(await color.value);
    }

    async enabledCheckForDoneButton()
    {
        await expect(await this.buttonDone).toBeEnabled();
    }

    async disabledCheckForDoneButton()
    {
        await expect(await this.buttonDone).toBeDisabled();
    }

    async verifyChildAgeBoxNotDisplayed(roomNumber)
    {
        await expect(await this.childInput(roomNumber, 1)).not.toBeDisplayed();
    }

    async verifyGuestsErrors() {
        const redColorSeries = Color.error;
        var color = await this.buttonGuests.getCSSProperty('outline-color')
        expect(redColorSeries).toContain(await color.value);
        color = await this.buttonGuests.getCSSProperty('color')
        expect(redColorSeries).toContain(await color.value);
        color = await this.buttonGuests.getCSSProperty('text-decoration-color')
        expect(redColorSeries).toContain(await color.value);
    }

    //#endregion[Guests Method]

    //#region [Search Unit Tab]

    public async selectTab(tabName) {
        await (await this.productTabName(tabName)).waitForDisplayed({timeoutMsg:'Next month is not displyed'})
        await (await this.productTabName(tabName)).click();
    }
    //#endregion [Search Unit Tab]




    //#endregion

}
export default new SearchUnit;


