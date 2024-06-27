
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace WebApiMovies.Services.Implements
{
    public sealed class UploadFileService : IUploadFileService
    {
        private readonly Cloudinary _cloudinary;

        public UploadFileService(Cloudinary cloudinary)
        {
            this._cloudinary = cloudinary;
        }

        public async Task<ImageUploadResult> UpdateFileAsync(IFormFile file, string publicId)
        {
            ImageUploadResult uploadesResult;
            using (var streamRead = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, streamRead),
                    PublicId = publicId,
                    Overwrite = true,
                    Invalidate = true
                };

                uploadesResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadesResult;
        }

        public async Task<ImageUploadResult> UploadFileAsync(IFormFile file, string folderName)
        {
            ImageUploadResult uploadesResult;
            using (var readStream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, readStream),
                    Overwrite = true,
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "MoviesAPI/"+ folderName,
                };

                uploadesResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadesResult;
        }
    }
}

