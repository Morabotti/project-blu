﻿namespace ProjectBlu.Dto.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public UserResponse User { get; set; }
}
