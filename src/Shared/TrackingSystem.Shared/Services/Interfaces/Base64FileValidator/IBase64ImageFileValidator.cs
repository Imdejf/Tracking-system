namespace TrackingSystem.Shared.Services.Interfaces.Base64FileValidator
{
    public interface IBase64ImageFileValidator : IAbstractBase64FileValidatorFileStage<IBase64ImageFileValidatorValidationStage> { }
    public interface IBase64ImageFileValidatorValidationStage : IAbstractBase64FileValidatorValidationStage<IBase64ImageFileValidatorValidationStage>
    {
    }
}
