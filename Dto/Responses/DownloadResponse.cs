using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class DownloadResponse
{
    public byte[] Data { get; set; }
    public string ContentType { get; set; }
    public string Extension { get; set; }
}
