using FluentValidation.Results;
using IMS.Domain.Exceptions;

namespace IMS.Application.Exceptions
{
    public sealed class IMSValidationException : IMSException
    {
        public List<string> ValdationErrors { get; private set; }

        public IMSValidationException(ValidationResult validationResult, string message) : base(message)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValdationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
