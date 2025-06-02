using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MarketProject.Service.Helpers.Cloudinary;

public sealed class FileService : IFileService
{

    private Account _account;
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly CloudinarySettings _cloudinarySettings;

    public FileService(IOptions<CloudinarySettings> cloudOptions)
    {

        _cloudinarySettings = cloudOptions.Value;

        _account = new Account(
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
        );

        _cloudinary = new(_account);
        _cloudinary.Api.Client.Timeout = TimeSpan.FromMinutes(30);
    }
    public async Task<string> UploadImageAsync(IFormFile formFile, string folderName)
    {
        var uploadResult = new ImageUploadResult();

        if (formFile.Length > 0)
        {

            using var stream = formFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formFile.FileName, stream),
                Folder = folderName
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            string imageUrl = _cloudinary.Api.UrlImgUp.BuildUrl(uploadResult.PublicId);
            return imageUrl;
        }

        return string.Empty;

    }
}