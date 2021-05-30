using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }
        public bool AllowNonMembers { get; set; }
        public bool RequirePermission { get; set; }
        public RulesetViewModel Ruleset { get; set; }
        public IEnumerable<BracketShortViewModel> Brackets { get; set; }

        public static implicit operator TournamentViewModel(Tournament tournament)
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

            return new TournamentViewModel
            {
                Id = tournament.Id,
                CreatedAt = tournament.CreatedAt.ToString("dd/MM/yyyy"),
                Name = tournament.Name,
                StartDate = tournament.StartDate.ToString("dd/MM/yyyy"),
                Status = status,
                AllowNonMembers = tournament.AllowNonMembers,
                RequirePermission = tournament.RequirePermission,
                Ruleset = tournament.Ruleset,
                Brackets = tournament.Brackets.OrderByDescending(b => b.Sequence).Select(b => (BracketShortViewModel) b)
            };
        }
    }
}