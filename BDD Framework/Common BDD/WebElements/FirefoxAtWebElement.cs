using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class FirefoxAtWebElement : AtWebElement
    {
        private readonly IWebElement _element;

        public FirefoxAtWebElement(IWebElement element = null) : base(element)
        {
            _element = element;
        }

        public new string Text => _element.Text;
    }
}