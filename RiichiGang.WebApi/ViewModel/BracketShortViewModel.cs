using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketShortViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string StartedAt { get; set; }
        public string FinishedAt { get; set; }
        public int Sequence { get; set; }
        public IEnumerable<BracketPlayerShortViewModel> Players { get; set; }

        public static implicit operator BracketShortViewModel(Bracket bracket)
        {
            if (bracket is null)
                return null;

            return new BracketShortViewModel
            {
                Id = bracket.Id,
                Name = bracket.Name,
                CreatedAt = bracket.CreatedAt.ToString("dd/MM/yyyy"),
                StartedAt = bracket.StartedAt?.ToString("dd/MM/yyyy"),
                FinishedAt = bracket.FinishedAt?.ToString("dd/MM/yyyy"),
                Sequence = bracket.Sequence,
                Players = bracket.Players.OrderBy(p => p.Placement).Select(p => (BracketPlayerShortViewModel) p)
            };
        }
    }
}