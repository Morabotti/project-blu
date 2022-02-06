using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IAssetService
{
    Task<ImageAsset> AddAutomaticImageAsync(ImageAsset image, string fileName);
}
