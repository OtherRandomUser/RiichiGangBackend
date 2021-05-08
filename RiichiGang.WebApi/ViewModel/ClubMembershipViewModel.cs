using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class ClubMembershipViewModel
    {
        public string CreatedAt { get; set; }
        public UserShortViewModel User { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }

        public static implicit operator ClubMembershipViewModel(Membership membership)
        {
            if (membership is null)
                return null;


            return new ClubMembershipViewModel
            {
                CreatedAt = membership.CreatedAt.ToString("dd/MM/yyyy"),
                User = membership.User,
                Approved = membership.Status == MembershipStatus.Confirmed,
                Denied = membership.Status == MembershipStatus.Denied
            };
        }
    }
}