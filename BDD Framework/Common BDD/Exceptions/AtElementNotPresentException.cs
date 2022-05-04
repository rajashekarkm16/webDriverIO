using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Dnata.Automation.BDDFramework.Enums;

namespace Dnata.Automation.BDDFramework.Exceptions
{
    [Serializable]
    public class AtElementNotPresentException : Exception
    {
        public string ElementSelector { get; set; }

        public AtExceptionTypes ErrorContext { get; set; }

        public AtElementNotPresentException()
        {
           
        }

        public AtElementNotPresentException(string message)
            : base(message)
        {
           
        }

        public AtElementNotPresentException(string message, Exception inner)
            : base(message, inner)
        {
           
        }

        protected AtElementNotPresentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ElementSelector = info.GetString("ElementSelector");
            ErrorContext = (AtExceptionTypes)Enum.Parse(typeof(AtExceptionTypes), info.GetString("ErrorContext"));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue("ErrorContext", ErrorContext);
            base.GetObjectData(info, context);
        }
    }

}