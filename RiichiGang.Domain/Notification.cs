using System;

namespace RiichiGang.Domain
{
    public class Notification : Entity
    {
        public string Message { get; protected set; }

        public User Creator { get; protected set; }
        public int CreatorId { get; protected set; }

        public User User { get; protected set; }
        public int UserId { get; protected set; }

        public Membership Membership { get; protected set; }
        public int? MembershipId { get; protected set; }

        public TournamentPlayer TournamentPlayer { get; protected set; }
        public int? TournamentPlayerId { get; protected set; }

        private Notification()
        {
        }

        public Notification(string message, User creator, User user, Membership membership, TournamentPlayer tournamentPlayer)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(
                    "A mensagem de uma notificação não pode ser nula");

            Message = message;

            CreatorId = creator?.Id ?? throw new ArgumentNullException(
                "O usuário criador de uma notificação não pode ser nulo");
            Creator = creator;

            UserId = user?.Id ?? throw new ArgumentNullException(
                "O usuário alvo de uma notificação não pode ser nulo");
            User = user;

            MembershipId = membership?.Id;
            Membership = membership;

            TournamentPlayerId = tournamentPlayer?.Id;
            TournamentPlayer = tournamentPlayer;
        }
    }
}