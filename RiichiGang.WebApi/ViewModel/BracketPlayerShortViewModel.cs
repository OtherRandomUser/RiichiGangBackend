using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketPlayerShortViewModel
    {
        public string Name { get; set; }
        public int Placement { get; set; }
        public float Score { get; set; }

        public static implicit operator BracketPlayerShortViewModel(BracketPlayer player)
        {
            if (player is null)
                return null;

            return new BracketPlayerShortViewModel
            {
                Name = player.Player.User.Username,
                Placement = player.Placement,
                Score = player.Score
            };
        }
    }
}