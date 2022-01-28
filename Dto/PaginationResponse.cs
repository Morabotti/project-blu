namespace ProjectBlu.Dto;

public class PaginationResponse<T>
{
    public long Length { get; set; }

    public IEnumerable<T> Result { get; set; }

    public PaginationResponse(IEnumerable<T> result, long length)
    {
        Result = result;
        Length = length;
    }
}
