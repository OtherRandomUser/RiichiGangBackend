using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketPlayerShortViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Placement { get; set; }
        public float Score { get; set; }

        public static implicit operator BracketPlayerShortViewModel(BracketPlayer player)
        {
            if (player is null)
                return null;

            return new BracketPlayerShortViewModel
            {
                UserId = player.Player.UserId,
                Name = player.Player.User.Username,
                Placement = player.Placement,
                Score = player.Score
            };
        }
    }
}