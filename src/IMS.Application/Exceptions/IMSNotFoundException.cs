using IMS.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Exceptions
{
    public sealed class IMSNotFoundException : IMSException
    {
        public IMSNotFoundException(string message) : base(message)
        {
        }
    }
}
