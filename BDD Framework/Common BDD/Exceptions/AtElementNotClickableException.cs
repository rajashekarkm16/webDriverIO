using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Dnata.Automation.BDDFramework.Enums;


namespace Dnata.Automation.BDDFramework.Exceptions
{
    [Serializable]
    public class AtElementNotClickableException : Exception
    {
      

        public string ElementSelector { get; set; }

        public AtExceptionTypes ErrorContext { get; set; }

        public AtElementNotClickableException()
        {
            
        }

        public AtElementNotClickableException(string message)
            : base(message)
        {
            
        }

        public AtElementNotClickableException(string message, Exception inner)
            : base(message, inner)
        {
            
        }

        protected AtElementNotClickableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ElementSelector = info.GetString("ElementSelector");
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
            base.GetObjectData(info, context);
        }
    }
}