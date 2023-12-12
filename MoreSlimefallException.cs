using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall
{
    public class MoreSlimefallException : Exception
    {
        public MoreSlimefallException() : base() { }

        public MoreSlimefallException(string message) : base(message) { }

        public MoreSlimefallException(string message, Exception innerException) : base(message, innerException) { }

        protected MoreSlimefallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
