using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class RemoteAtWebElement : AtWebElement
    {
        private readonly IWebElement _element;

        public RemoteAtWebElement(IWebElement element = null) : base(element)
        {
            _element = element;
        }

        public new string Text => _element.Text;
    }
}