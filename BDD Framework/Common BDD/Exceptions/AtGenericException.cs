using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Dnata.Automation.BDDFramework.Enums;

namespace Dnata.Automation.BDDFramework.Exceptions
{
    [Serializable]
    public class AtGenericException : Exception
    {
        public AtExceptionTypes ErrorContext { get; set; }

        public AtGenericException()
        {
        }

        public AtGenericException(string message)
            : base(message)
        {
        }

        public AtGenericException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AtGenericException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
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