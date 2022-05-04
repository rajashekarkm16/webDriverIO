using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Dnata.Automation.BDDFramework.Enums;

namespace Dnata.Automation.BDDFramework.Exceptions
{
    [Serializable]
    public class AtTextNotPresentException : Exception
    { 

        public string ElementSelector { get; set; }

        public string SearchText { get; set; }

        public AtExceptionTypes ErrorContext { get; set; }

        public string ScreenshotFileName { get; set; }


        public AtTextNotPresentException()
        {
           // ScreenshotFileName = _screenShotHandler.TakeScreenshot();
        }

        public AtTextNotPresentException(string message)
            : base(message)
        {
        }

        public AtTextNotPresentException(string message, Exception inner)
            : base(message, inner)
        {
           // ScreenshotFileName = _screenShotHandler.TakeScreenshot();
        }

        protected AtTextNotPresentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ElementSelector = info.GetString("ElementSelector");
            SearchText = info.GetString("SearchText");
            ErrorContext = (AtExceptionTypes)Enum.Parse(typeof(AtExceptionTypes), info.GetString("ErrorContext"));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ErrorContext", ErrorContext);
            info.AddValue("SearchText", SearchText);
            info.AddValue("ElementSelector", ElementSelector);
            info.AddValue("ScreenshotFileName", ScreenshotFileName);
            base.GetObjectData(info, context);
        }
    }

}