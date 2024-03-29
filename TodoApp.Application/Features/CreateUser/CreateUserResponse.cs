﻿namespace TodoApp.Application.Features.CreateUser;

public class CreateUserResponse : IResponse
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}