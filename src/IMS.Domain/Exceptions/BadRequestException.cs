using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Exceptions
{
    public class BadRequestException : IMSException
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
