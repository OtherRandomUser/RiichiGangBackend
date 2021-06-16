using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class TournamentPlayerViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }

        public static implicit operator TournamentPlayerViewModel(TournamentPlayer player)
        {
            if (player is null)
                return null;

            var status = "";

            switch (player.Status)
            {
            case TournamentPlayerStatus.Pending:
                status = "Pendente";
                break;

            case TournamentPlayerStatus.Confirmed:
                status = "Confirmado";
                break;

            case TournamentPlayerStatus.Denied:
                status = "Negado";
                break;
            }

            return new TournamentPlayerViewModel
            {
                UserId = player.User.Id,
                Username = player.User.Username,
                Status = status
            };
        }
    }
}
