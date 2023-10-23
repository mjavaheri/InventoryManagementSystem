using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Products.Commands.UpdateInventoryCount
{
    public sealed class UpdateInventoryCountCommand : IRequest
    {
        public int Value { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
