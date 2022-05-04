using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class IeAtWebElement : AtWebElement
    {
        private readonly IWebElement _element;

        public IeAtWebElement(IWebElement element = null) : base(element)
        {
            _element = element;
        }

        public new string Text => _element.Text;
    }
}