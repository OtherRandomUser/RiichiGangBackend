using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketViewModel
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public int Sequence { get; set; }
        public IEnumerable<BracketPlayerViewModel> Players { get; set; }

        public static implicit operator BracketViewModel(Bracket bracket)
        {
            if (bracket is null)
                return null;

            return new BracketViewModel
            {
                Id = bracket.Id,
                CreatedAt = bracket.CreatedAt.ToString("dd/MM/yyyy"),
                Sequence = bracket.Sequence,
                Players = bracket.Players.OrderBy(p => p.Placement).Select(p => (BracketPlayerViewModel) p)
            };
        }
    }
}