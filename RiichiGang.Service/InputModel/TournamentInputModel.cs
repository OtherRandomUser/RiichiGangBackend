using System;
using System.Collections.Generic;

namespace RiichiGang.Service.InputModel
{
    public class TournamentInputModel
    {
        public int RulesetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public bool AllowNonMembers { get; set; }
        public bool RequirePermission { get; set; }
        public IEnumerable<BracketInputModel> Brackets { get; set; }
    }
}