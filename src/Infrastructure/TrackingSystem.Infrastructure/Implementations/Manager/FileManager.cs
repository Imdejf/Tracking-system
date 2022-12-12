using TrackingSystem.Application.Common.Interfaces.Manager;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Infrastructure.Implementations.Manager
{
    internal sealed class FileManager : IFileManager
    {
        public FileManager()
        {

        }

        public void DeleteFile(string path)
        {
            var basePath = Directory.GetCurrentDirectory() + $@"\wwwroot{path}" ;
            if (!System.IO.File.Exists(basePath))
            {
                throw new ArgumentException("file not exist");
            }

            System.IO.File.Delete(basePath);
        }

        public bool ExistFile(string path)
        {
            var basePath = Directory.GetCurrentDirectory() + @"\wwwroot";
            basePath += path;

            if (!File.Exists(basePath))
            {
                return false;
            }
            return true;
        }

        public string SaveFile(SaveType saveType, string fileName, Base64File file)
        {
            var path = Directory.GetCurrentDirectory() + @"\wwwroot";
            path += $@"\{saveType.ToString()}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += $@"\{fileName}.{file.FileExtension}";
            System.IO.File.WriteAllBytes(path, Convert.FromBase64String(file.Base64String));
            path = path.ToString().Split(@"\wwwroot").Last();

            return path;
        }
    }
}
