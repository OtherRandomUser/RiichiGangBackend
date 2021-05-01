using System;

namespace RiichiGang.Domain
{
    public class Notification : Entity
    {
        public string Message { get; protected set; }

        public User User { get; protected set; }
        public int UserId { get; protected set; }

        public User Requester { get; protected set; }
        public int RequesterId { get; protected set; }

        public Membership Membership { get; protected set; }
        public int? MembershipId { get; protected set; }

        private Notification()
        {
        }

        public Notification(string message, User user, User requester, Membership membership)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(
                    "A mensagem de uma notificação não pode ser nula");

            Message = message;

            UserId = user?.Id ?? throw new ArgumentNullException(
                "O usuário alvo de uma notificação não pode ser nulo");
            User = user;

            RequesterId = requester?.Id ?? throw new ArgumentNullException(
                "O usuário criador de uma notificação não pode ser nulo");
            Requester = requester;

            MembershipId = membership?.Id;
            Membership = membership;
        }
    }
}