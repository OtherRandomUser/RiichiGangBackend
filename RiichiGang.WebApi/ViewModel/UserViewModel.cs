using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public sealed class UserViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public static implicit operator UserViewModel(User user)
        {
            if (user is null)
                return null;

            return new UserViewModel
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}