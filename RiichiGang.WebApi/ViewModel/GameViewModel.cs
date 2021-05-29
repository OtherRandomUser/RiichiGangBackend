using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class GameViewModel
    {
        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        public PlayerViewModel Player3 { get; set; }
        public PlayerViewModel Player4 { get; set; }
        public string PlayedAt { get; set; }
        public string Log { get; set; }

        public static implicit operator GameViewModel(Game game)
        {
            if (game is null)
                return null;

            return new GameViewModel
            {
                Player1 = game.Player1,
                Player2 = game.Player2,
                Player3 = game.Player3,
                Player4 = game.Player4,
                PlayedAt = game.PlayedAt?.ToString("dd/MM/yyyy"),
                Log = game.LogLink,
            };
        }
    }

    public class PlayerViewModel
    {
        public string Seat { get; set; }
        public float GameScore { get; set; }
        public float RunningTotal { get; set; }

        public static implicit operator PlayerViewModel(Player player)
        {
            if (player is null)
                return null;

            var seat = default(string);

            switch (player.Seat)
            {
            case Domain.Seat.East:
                seat = "東";
                break;

            case Domain.Seat.South:
                seat = "南";
                break;

            case Domain.Seat.West:
                seat = "西";
                break;

            case Domain.Seat.North:
                seat = "北";
                break;
            }

            return new PlayerViewModel
            {
                Seat = seat,
                GameScore = player.EndScore,
                RunningTotal = player.RunningTotal
            };
        }
    }
}