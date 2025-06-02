using Microsoft.AspNetCore.Http;

namespace MarketProject.Service.Helpers.Cloudinary;
public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile formFile, string folderName);
} 