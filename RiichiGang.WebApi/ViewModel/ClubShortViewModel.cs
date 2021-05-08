using System;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class ClubShortViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Contact { get; set; }
        public string Localization { get; set; }
        public int TotalPlayers { get; set; }
        public int TotalTournaments { get; set; }

        public static implicit operator ClubShortViewModel(Club club)
        {
            if (club is null)
                return null;

            return new ClubShortViewModel
            {
                Id = club.Id,
                CreatedAt = club.CreatedAt.ToString("dd/MM/yyyy"),
                Name = club.Name,
                Website = club.Website,
                Contact = club.Contact,
                Localization = club.Localization,
                TotalPlayers = club.Members?.Count() + 1 ?? 0,
                TotalTournaments = 0
            };
        }
    }
}