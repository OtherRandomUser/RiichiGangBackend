using System;

namespace RiichiGang.Domain
{
    public class Membership : Entity
    {
        public User User { get; protected set; }
        public Guid UserId { get; protected set; }

        public Club Club { get; protected set; }
        public Guid ClubId { get; protected set; }

        public MembershipStatus Status { get; set; }

        private Membership()
        {
        }

        public Membership(User user, Club club)
        {
            UserId = user?.Id ?? throw new ArgumentNullException(
                "O usuário alvo de uma filiação não pode ser nulo");
            User = user;

            ClubId = club?.Id ?? throw new ArgumentNullException(
                "O clube alvo de uma filiação não pode ser nulo");
            Club = club;

            Status = MembershipStatus.Pending;
        }
    }

    public enum MembershipStatus
    {
        Pending,
        Confirmed,
        Denied
    }
}