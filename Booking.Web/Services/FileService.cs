using Booking.Web.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Booking.Web.Services
{
    internal sealed class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileService> _logger;
        private readonly string _webRootPath;
        public IList<string> Files { get; set; }

        public FileService(
            IWebHostEnvironment webHostEnvironment, 
            ILogger<FileService> logger)
        {
            Files= new List<string>();
            _webHostEnvironment= webHostEnvironment;
            _logger= logger;
            _webRootPath = _webHostEnvironment.WebRootPath;
        }
        public void DeleteFile(IList<string> fileNames)
        {
            try
            {
                foreach(string fileName in fileNames) 
                {
                    if (!fileName.IsNullOrEmpty())
                    {
                        string filePath = Path.Combine(_webRootPath, fileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting file failed");
            }
        }

        public void UploadFiles(IFormFileCollection files, string dirPath)
        {
            try
            { 
                foreach (var file in files) 
                {
                    string relativePath = Path.Combine(dirPath, $"{Guid.NewGuid().ToString()}-{file.FileName}");
                    string filePath = Path.Combine(_webRootPath, relativePath);
                    Files.Add(relativePath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uploading file failed");
            }
        }
    }
}
