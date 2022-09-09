using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Application.Common.Interfaces.Manager
{
    public interface IFileManager
    {
        string SaveFile(SaveType saveType, string fileName, Base64File file);
        bool ExistFile(string path);
        void DeleteFile(string path);
    }
}
