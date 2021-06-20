using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class SeriesViewModel
    {
        public int Id { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string Player3Name { get; set; }
        public string Player4Name { get; set; }
        public string PlayedAt { get; set; }
        public string FinishedAt { get; set; }
        public string Status { get; set; }

        public IEnumerable<GameViewModel> Games { get; set; }

        public static implicit operator SeriesViewModel(Series series)
        {
            if (series is null)
                return null;

            var games = series.Games.Select(g => (GameViewModel) g);

            var playedAt = games.FirstOrDefault().PlayedAt;
            var finishedAt = games.LastOrDefault().PlayedAt;
            var status = "Agendada";

            if (playedAt != null)
                status = "Em Andamento";

            if (finishedAt != null)
                status = "Encerrada";

            return new SeriesViewModel
            {
                Id = series.Id,
                Player1Name = series.Player1.Player.User.Username,
                Player2Name = series.Player2.Player.User.Username,
                Player3Name = series.Player3.Player.User.Username,
                Player4Name = series.Player4.Player.User.Username,
                PlayedAt = playedAt,
                FinishedAt = finishedAt,
                Status = status,
                Games = games
            };
        }
    }
}