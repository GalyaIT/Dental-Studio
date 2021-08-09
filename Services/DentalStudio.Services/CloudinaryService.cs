namespace DentalStudio.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DentalStudio.Common;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPhotoAsync(IFormFile file, string fileName, string folder)
        {
            byte[] destinationData;
            UploadResult uploadResult = null;
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationData = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream(destinationData))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams
                    {

                        Folder = folder,
                        File = new FileDescription(fileName, memoryStream),
                    };

                    uploadResult = this.cloudinaryUtility.Upload(uploadParams);
                }
            }
            else
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = folder,
                    File = new FileDescription(GlobalConstants.DefaultPhoto),
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}