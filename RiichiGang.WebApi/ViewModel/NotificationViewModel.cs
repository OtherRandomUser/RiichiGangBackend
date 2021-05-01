using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        public int? MembershipId { get; set; }
        public UserShortViewModel Requester { get; set; }

        public static implicit operator NotificationViewModel(Notification notification)
        {
            if (notification is null)
                return null;

            return new NotificationViewModel
            {
                Id = notification.Id,
                CreatedAt = notification.CreatedAt,
                Message = notification.Message,
                MembershipId = notification.MembershipId,
                Requester = notification.Requester
            };
        }
    }
}