using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Models;

namespace ProjectBlu.Services;

public class AssetService : IAssetService
{
    private readonly ProjectBluContext _context;
    private readonly string _staticFolderPath;

    public AssetService(
        ProjectBluContext context,
        IConfiguration configuration
    )
    {
        var folderName = configuration.GetValue<string>("StaticFolder");
        _context = context;
        _staticFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
    }

    public async Task<ImageAsset> AddAutomaticImageAsync(ImageAsset image, string fileName)
    {
        if (image == null || image.Source == null)
        {
            return null;
        }

        CreateDirectory();

        var download = await DownloadImageAsync(image.Source);
        var rawName = $"{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid()}-{Guid.NewGuid():N}{download.Extension}";
        var nameWithExt = $"{fileName}{download.Extension}";

        var updatedAsset = new ImageAsset
        {
            Source = rawName,
            FileName = nameWithExt,
            ContentType = download.ContentType,
            Hash = null
        };

        await UploadFileAsync(download.Data, updatedAsset.Source);

        return updatedAsset;
    }

    private static async Task<DownloadResponse> DownloadImageAsync(string url)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(url);

        var headers = response.Content.Headers.ContentType.MediaType;
        var type = response.Content.Headers.ContentType.MediaType;
        var disposition = response.Content.Headers.ContentDisposition.FileName?.Replace("\"", "");
        var data = await response.Content.ReadAsByteArrayAsync();
        var extension = Path.GetExtension(disposition);

        return new DownloadResponse
        {
            ContentType = type,
            Data = data,
            Extension = extension
        };
    }

    private void CreateDirectory()
    {
        bool exists = Directory.Exists(_staticFolderPath);

        if (!exists)
        {
            Directory.CreateDirectory(_staticFolderPath);
        }
    }

    private void DeleteFileIfExist(string rawName)
    {
        var filePath = Path.Combine(_staticFolderPath, rawName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private async Task UploadFileAsync(byte[] data, string rawName)
    {
        var filePath = Path.Combine(_staticFolderPath, rawName);
        using var fs = new FileStream(filePath, FileMode.Create);
        await fs.WriteAsync(data, 0, data.Length);
    }
}
