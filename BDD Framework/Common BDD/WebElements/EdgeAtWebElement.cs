using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Exceptions;
using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class EdgeAtWebElement : AtWebElement
    {
        private readonly IWebElement _element;

        public EdgeAtWebElement(IWebElement element = null) : base(element)
        {
            _element = element;
        }

        public new string Text => _element.Text;

        public override void SelectDropdownOptionByValue(string optionValue)
        {
            try
            {
                SendKeys(optionValue);
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = TagName,
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }
    }
}