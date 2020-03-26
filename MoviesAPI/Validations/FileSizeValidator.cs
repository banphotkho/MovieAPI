using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class FileSizeValidator : ValidationAttribute
    {
        private readonly int maxFileSizeInMbs;

        public FileSizeValidator(int MaxFileSizeInMbs)
        {
            maxFileSizeInMbs = MaxFileSizeInMbs;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //Check value is null return validate=success
            if (value == null)
            {
                return ValidationResult.Success;
            }


            IFormFile fromFile = value as IFormFile;

            if (fromFile == null)
            {
                return ValidationResult.Success;
            }

            if(fromFile.Length > maxFileSizeInMbs * 1024 * 1204)
            {
                return new ValidationResult($"File size can't be biger than {maxFileSizeInMbs} megabytes");
            }

            return ValidationResult.Success;

        }
    }
}
