using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public sealed class UserViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public StatsViewModel Stats { get; set; }
        public IEnumerable<ClubShortViewModel> OwnedClubs { get; set; }
        public IEnumerable<MembershipViewModel> Memberships { get; set; }
        public IEnumerable<TournamentViewModel> Tournaments { get; set; }
        public IEnumerable<NotificationViewModel> Notifications { get; set; }

        public static implicit operator UserViewModel(User user)
        {
            if (user is null)
                return null;

            return new UserViewModel
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Username = user.Username,
                Email = user.Email,
                Stats = user.Stats,
                OwnedClubs = user.OwnedClubs.Select(c => (ClubShortViewModel) c),
                Tournaments = user.Tournaments.Select(t => (TournamentViewModel) t),
                Notifications = user.Notifications.Select(n => (NotificationViewModel) n)
            };
        }
    }
}