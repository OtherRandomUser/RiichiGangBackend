using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class ClubShortViewModel
    {
        int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Contact { get; set; }

        public static implicit operator ClubShortViewModel(Club club)
        {
            if (club is null)
                return null;

            return new ClubShortViewModel
            {
                Id = club.Id,
                CreatedAt = club.CreatedAt,
                Name = club.Name,
                Website = club.Website,
                Contact = club.Contact
            };
        }
    }
}