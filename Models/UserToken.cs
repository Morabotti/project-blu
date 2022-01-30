namespace ProjectBlu.Models;

public enum TokenType
{
    ResetPassword = 0
}

public class UserToken
{
    public long Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public TokenType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
