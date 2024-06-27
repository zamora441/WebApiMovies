
using CloudinaryDotNet.Actions;

namespace WebApiMovies.Services
{
    public interface IUploadFileService
    {
        //Task UploadFile(DropboxClient dbx, string folder, string file, string content);
        Task<ImageUploadResult> UploadFileAsync(IFormFile file, string folderName);
        Task<ImageUploadResult> UpdateFileAsync(IFormFile file, string fileId);
    }
}
