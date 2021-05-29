using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class ClubViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Contact { get; set; }
        public string Localization { get; set; }

        public UserShortViewModel Owner { get; set; }
        public IEnumerable<ClubMembershipViewModel> Members { get; set; }
        public IEnumerable<TournamentShortViewModel> Tournaments { get; set; }

        public static implicit operator ClubViewModel(Club club)
        {
            if (club is null)
                return null;

            return new ClubViewModel
            {
                Id = club.Id,
                CreatedAt = club.CreatedAt.ToString("dd/MM/yyyy"),
                Name = club.Name,
                Website = club.Website,
                Contact = club.Contact,
                Localization = club.Localization,
                Owner = club.Owner,
                Members = club.Members?.Select(m => (ClubMembershipViewModel) m),
                Tournaments = club.Tournaments?.Select(t => (TournamentShortViewModel) t)
            };
        }
    }
}