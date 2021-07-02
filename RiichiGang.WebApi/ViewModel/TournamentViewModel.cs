using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }
        public bool AllowNonMembers { get; set; }
        public bool RequirePermission { get; set; }
        public string PlayerStatus { get; set; }
        public RulesetViewModel Ruleset { get; set; }
        public IEnumerable<TournamentPlayerViewModel> Players { get; set; }
        public IEnumerable<BracketShortViewModel> Brackets { get; set; }

        public TournamentViewModel(Tournament tournament, User user)
        {
            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

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

            Id = tournament.Id;
            ClubId = tournament.ClubId;
            CreatedAt = tournament.CreatedAt.ToString("dd/MM/yyyy");
            OwnerId = tournament.Club.OwnerId;
            Name = tournament.Name;
            Description = tournament.Description;
            StartDate = tournament.StartDate.ToString("dd/MM/yyyy");
            Status = status;
            AllowNonMembers = tournament.AllowNonMembers;
            RequirePermission = tournament.RequirePermission;
            Ruleset = tournament.Ruleset;
            Players = tournament.Players.Select(p => (TournamentPlayerViewModel) p);
            Brackets = tournament.Brackets.OrderByDescending(b => b.Sequence).Select(b => (BracketShortViewModel) b);

            if (user is null)
            {
                PlayerStatus = "";
            }
            else
            {
                var player = tournament.Players.SingleOrDefault(p => p.Id == user.Id);

                if (player is null)
                {
                    PlayerStatus = "";
                }
                else
                {
                    switch (player.Status)
                    {
                    case TournamentPlayerStatus.Pending:
                        PlayerStatus = "Pendente";
                        break;

                    case TournamentPlayerStatus.Confirmed:
                        PlayerStatus = "Confirmado";
                        break;

                    case TournamentPlayerStatus.Denied:
                        PlayerStatus = "Negado";
                        break;
                    }
                }
            }

            if (tournament.Status == TournamentStatus.Scheduled)
            {
                var Players = tournament.Players.Select(p => (TournamentPlayerViewModel) p);

                if (user?.Id != tournament.Club.OwnerId)
                {
                    Players = Players.Where(p => p.Status == "Confirmado");
                }
            }
        }
    }
}