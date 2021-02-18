using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            Errors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                Errors.Add(validationError.ErrorMessage);
            }
        }
    }
}
