using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
    }
}
