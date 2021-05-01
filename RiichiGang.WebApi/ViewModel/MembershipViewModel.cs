using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class MembershipViewModel
    {
        public ClubShortViewModel Club { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }

        public static implicit operator MembershipViewModel(Membership membership)
        {
            if (membership is null)
                return null;


            return new MembershipViewModel
            {
                Club = membership.Club,
                Approved = membership.Status == MembershipStatus.Confirmed,
                Denied = membership.Status == MembershipStatus.Denied
            };
        }
    }
}