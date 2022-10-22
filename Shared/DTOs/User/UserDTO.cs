using System;

namespace Shared.DTOs.User
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
    }
}
