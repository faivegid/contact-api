using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contact.Models.DTOs;
using Contact.Repository.Implementaions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Logic.UploadImage
{
    public class ImageUploader : IImageUploader
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        private readonly ImageSettingDTO _imageSetting;
        public ImageUploader(IConfiguration configuration, IOptions<ImageSettingDTO> imageSettings)
        {
            _configuration = configuration;
            _imageSetting = imageSettings.Value;
            _cloudinary = new Cloudinary(
                    new Account(_imageSetting.AccountName, _imageSetting.ApiKey, _imageSetting.ApiSecret)
                );
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile image)
        {
            var result = CheckImageFormat(image);
            var uploadResult = new ImageUploadResult();

            using (var imageStream = image.OpenReadStream())
            {
                var filename = Guid.NewGuid().ToString() + image.FileName;
                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(filename, imageStream),
                    Transformation = new Transformation().SetHtmlHeight(250).SetHtmlWidth(250)
                });
            }
            return uploadResult;
        }

        private bool CheckImageFormat(IFormFile image)
        {
            var listOfImageFormats = _configuration.GetSection("ImageSettings:ImageFormat").Get<List<string>>();
            foreach (var item in listOfImageFormats)
            {
                if (image.FileName.EndsWith(item))
                {
                    return true;
                }
            }
            throw new NotSupportedException("File format not supported");
        }
    }
}
