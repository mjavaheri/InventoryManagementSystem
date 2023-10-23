using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Products.Commands.UpdateInventoryCount
{
    public sealed class UpdateInventoryCountCommandValidator : AbstractValidator<UpdateInventoryCountCommand>
    {
        public UpdateInventoryCountCommandValidator() 
        {
            RuleFor(p => p.Value)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.RowVersion)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
