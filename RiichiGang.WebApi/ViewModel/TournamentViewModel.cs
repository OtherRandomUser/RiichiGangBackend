using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public static implicit operator TournamentViewModel(Tournament tournament)
        {
            if (tournament is null)
                return null;

            return new TournamentViewModel
            {
                Id = tournament.Id,
                CreatedAt = tournament.CreatedAt
            };
        }
    }
}