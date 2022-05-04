using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Dnata.Automation.BDDFramework.Enums;

namespace Dnata.Automation.BDDFramework.Exceptions
{
    [Serializable]
    public class AtConfigurationException : Exception
    {
        public string Key { get; set; }

        public string BuildConfig { get; set; }

        public AtExceptionTypes ErrorContext { get; set; }

        public AtConfigurationException()
        {
        }

        public AtConfigurationException(string message)
            : base(message)
        {
        }

        public AtConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AtConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Key = info.GetString("Key");
            BuildConfig = info.GetString("BuildConfig");
            ErrorContext = (AtExceptionTypes) Enum.Parse(typeof(AtExceptionTypes), info.GetString("ErrorContext"));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("ErrorContext", ErrorContext);
            base.GetObjectData(info, context);
        }
    }
}