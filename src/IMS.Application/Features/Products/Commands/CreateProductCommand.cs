﻿using MediatR;

namespace IMS.Application.Features.Products.Commands
{
    public sealed class CreateProductCommand : IRequest<int>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public byte Discount { get; set; }
    }
}
