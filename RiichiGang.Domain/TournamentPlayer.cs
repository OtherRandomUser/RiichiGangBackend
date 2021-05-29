using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class TournamentPlayer : Entity
    {
        public User User { get; protected set; }
        public int UserId { get; protected set; }

        public Tournament Tournament { get; protected set; }
        public int TournamentId { get; protected set; }

        public TournamentPlayerStatus Status { get; set; }

        public IEnumerable<BracketPlayer> Brackets { get; protected set; }

        protected TournamentPlayer()
        {
        }

        public TournamentPlayer(User user, Tournament tournament, TournamentPlayerStatus status)
        {
            SetUser(user);
            SetTournament(tournament);

            Status = status;

            Brackets = new List<BracketPlayer>();
        }

        public void SetUser(User user)
        {
            UserId = user?.Id ?? throw new ArgumentNullException("O usuário participante de um torneio não pode ser nulo");
            User = user;
        }

        public void SetTournament(Tournament tournament)
        {
            TournamentId = tournament?.Id ?? throw new ArgumentNullException("O torneio não pode ser nulo");
            Tournament = tournament;
        }
    }

    public enum TournamentPlayerStatus
    {
        Pending,
        Confirmed,
        Denied
    }
}