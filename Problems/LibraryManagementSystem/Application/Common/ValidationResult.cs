using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Application.Common
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; }

        private ValidationResult(List<string> errors)
        {
            Errors = errors;
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(new List<string>());
        }

        public static ValidationResult Failure(params string[] errors)
        {
            return new ValidationResult(errors.ToList());
        }

        public static ValidationResult Failure(List<string> errors)
        {
            return new ValidationResult(errors);
        }
    }
}