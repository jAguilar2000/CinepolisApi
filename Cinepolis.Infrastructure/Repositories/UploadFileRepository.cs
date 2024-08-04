using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Infrastructure.Repositories
{
    public class UploadFileRepository
    {
        public ImageUploadResult UploadFile(IFormFile file, string dir)
        {
            try
            {
                var cloudinary = new Cloudinary(new Account("dxweztzam", "122935397572425", "zOco24rA9SxbWpE57aDY12Q2GGs"));

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = dir + file.FileName,
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                //Transformation
                cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100).Height(150).Crop("fill")).BuildUrl("olympic_flag");

                return uploadResult;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public ImageUploadResult UploadBase64(ImagenViewModel img, string dir)
        {
            try
            {
                var cloudinary = new Cloudinary(new Account("dxweztzam", "122935397572425", "zOco24rA9SxbWpE57aDY12Q2GGs"));

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(img.ImgBase64),
                    PublicId = dir + img.Nombre,
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                //Transformation
                cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100).Height(150).Crop("fill")).BuildUrl("olympic_flag");

                return uploadResult;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
