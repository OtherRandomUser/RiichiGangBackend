using System;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class TournamentShortViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Name { get; set; }
        public int TotalPlayers { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }

        public static implicit operator TournamentShortViewModel(Tournament tournament)
        {
            if (tournament is null)
                return null;

            var status = "";

            switch (tournament.Status)
            {
            case TournamentStatus.Scheduled:
                status = "Agendado";
                break;

            case TournamentStatus.Running:
                status = "Em Andamento";
                break;

            case TournamentStatus.Finished:
                status = "Encerrado";
                break;
            }

            return new TournamentShortViewModel
            {
                Id = tournament.Id,
                CreatedAt = tournament.CreatedAt.ToString("dd/MM/yyyy"),
                Name = tournament.Name,
                TotalPlayers = tournament.Players?.Count() ?? 0,
                StartDate = tournament.StartDate.ToString("dd/MM/yyyy"),
                Status = status,
            };
        }
    }
}