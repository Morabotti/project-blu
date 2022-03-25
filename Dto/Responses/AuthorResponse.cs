namespace ProjectBlu.Dto.Responses;

public class AuthorResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserImageResponse Image { get; set; }
}
