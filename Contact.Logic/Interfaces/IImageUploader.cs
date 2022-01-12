using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Contact.Logic.UploadImage
{
    public interface IImageUploader
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile image);
    }
}