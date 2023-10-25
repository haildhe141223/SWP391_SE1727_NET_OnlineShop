using SWP391.OnlineShop.Common.Constraints;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.Core.Customs.Attributes
{
    public class AllowedImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedImageExtensionsAttribute()
        {
            _extensions = ImageConstraints.imageExtentionLists.ToArray();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Should insert library to apply for IFormFile - Http.Features
            //var file = value as IFormFile;
            var file = value as string;
            if (file != null)
            {
                var extension = Path.GetExtension(file);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
