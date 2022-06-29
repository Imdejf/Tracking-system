using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace TrackingSystem.Shared.Models
{
    public class Base64File : ValueObject
    {
        public string Base64String
        {
            get { return this._base64String; }
            init
            {
                if (value.Length % 4 != 0 || !Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
                {
                    throw new InvalidBase64FileException("Invalid base64 string passed");
                }
                this._base64String = value;
            }
        }
        private string _base64String;

        public Base64File()
        {

        }
        public Base64File(string base64String)
        {
            Base64String = base64String;
        }

        public string FileExtension => getFileExtension(_base64String);
        public float SizeInMb => (ByteArray.Length / 1024f) / 1024f;
        public byte[] ByteArray => Convert.FromBase64String(_base64String);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _base64String;
        }
        private string getFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            return data.ToUpper() switch
            {
                "IVBOR" => ".png",
                "/9J/4" => ".jpeg",
                "AAAAF" => ".mp4",
                "JVBER" => ".pdf",
                "AAABA" => ".ico",
                "UMFYI" => ".rar",
                "E1XYD" => ".rtf",
                "MQOWM" or "77U/M" => ".srt",
                "ZKXHQ" => ".flac",
                "UKLGR" => ".wav",
                "//UQZ" => ".mp3",
                "UEsDB" => ".xlsx",
                _ => ".txt"
            };
        }
    }
}
