using System.Collections;
using System.Collections.Generic;
using FluentValidation.Results;

namespace BikeShop.Infrastructure
{
    public class Result<T>
    {
        public T Value { get; set; }

        public IList<Error> Errors { get; } = new List<Error>();

        public void CombineErrors<TOther>(Result<TOther> other)
        {
            foreach (var error in other.Errors)
            {
                Errors.Add(error);
            }
        }

        public void CombineErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }

        public void CombineErrors(ValidationResult validationResult)
        {
            foreach (var validationError in validationResult.Errors)
            {
                Errors.Add(new Error(validationError.ErrorMessage));
            }
        }
    }
}
