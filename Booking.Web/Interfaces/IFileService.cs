namespace Booking.Web.Interfaces
{
    public interface IFileService
    {
        public IList<string> Files { get; set; }
        public void UploadFiles(IFormFileCollection files, string dirPath);
        public void DeleteFile(IList<string> fileNames);
    }
}
