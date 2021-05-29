using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketPlayerViewModel
    {
        public string Name { get; set; }
        public int Placement { get; set; }
        public float Score { get; set; }

        public static implicit operator BracketPlayerViewModel(BracketPlayer player)
        {
            if (player is null)
                return null;

            return new BracketPlayerViewModel
            {
                Name = player.Player.User.Username,
                Placement = player.Placement,
                Score = player.Score
            };
        }
    }
}