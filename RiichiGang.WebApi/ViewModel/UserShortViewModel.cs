using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public sealed class UserShortViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Username { get; set; }
        public int TotalTournaments { get; set; }

        public static implicit operator UserShortViewModel(User user)
        {
            if (user is null)
                return null;

            return new UserShortViewModel
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt.ToString("dd/MM/yyyy"),
                Username = user.Username,
                TotalTournaments = user.Tournaments?.Count() ?? 0
            };
        }
    }
}