using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces.Base64FileValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Shared.Services.Implementations.Base64FileValidator
{
    internal abstract class AbstractBase64FileValidator<TValidator> : IAbstractBase64FileValidatorFileStage<TValidator>,
                                                                       IAbstractBase64FileValidatorValidationStage<TValidator>
           where TValidator : IAbstractBase64FileValidatorValidationStage<TValidator>

    {
        protected List<string> _Errors = new();
        protected Base64File _FileToValidate;
        public AbstractBase64FileValidator()
        {
        }

        public TValidator OnFile(Base64File base64File)
        {
            _FileToValidate = base64File;
            return (TValidator)(IAbstractBase64FileValidatorValidationStage<TValidator>)this;
        }

        public TValidator WithExtension(params string[] extensions)
        {
            ensureFileToValidateIsNotNull();
            if (!extensions.Contains(_FileToValidate.FileExtension))
            {
                _Errors.Add("File has invalid extension");
            }
            return (TValidator)(IAbstractBase64FileValidatorValidationStage<TValidator>)this;
        }

        public TValidator WithMaxFileSize(double fileSizeInMb)
        {
            ensureFileToValidateIsNotNull();
            if (_FileToValidate.SizeInMb > fileSizeInMb)
            {
                _Errors.Add("File is to big");
            }
            return (TValidator)(IAbstractBase64FileValidatorValidationStage<TValidator>)this;
        }

        public TValidator WithMinFileSize(double fileSizeInMb)
        {
            ensureFileToValidateIsNotNull();
            if (_FileToValidate.SizeInMb < fileSizeInMb)
            {
                _Errors.Add("File is to small");
            }
            return (TValidator)(IAbstractBase64FileValidatorValidationStage<TValidator>)this;
        }

        public TValidator WithoutExtension(params string[] extensions)
        {
            ensureFileToValidateIsNotNull();
            if (extensions.Contains(_FileToValidate.FileExtension))
            {
                _Errors.Add("File has invalid extension");
            }
            return (TValidator)(IAbstractBase64FileValidatorValidationStage<TValidator>)this;
        }

        public bool IsValid(out string[] errors)
        {
            errors = _Errors.ToArray();
            return errors.Length == 0;
        }

        private void ensureFileToValidateIsNotNull()
        {
            if (_FileToValidate is null)
            {
                throw new InvalidOperationException($"FileToValidate can`t be NULL");
            }
        }
    }
}
