using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class ChromeAtWebElement : AtWebElement
    {
        private readonly IWebElement _element;

        public ChromeAtWebElement(IWebElement element = null) : base(element)
        {
            _element = element;
        }

        public new string Text => _element.Text;
    }
}