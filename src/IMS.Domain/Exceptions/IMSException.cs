using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Exceptions
{
    public abstract class IMSException : Exception
    {
        protected IMSException(string message) : base(message)
        {
        }
    }
}
