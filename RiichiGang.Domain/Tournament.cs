using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class Tournament : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public TournamentStatus Status { get; set; }
        public DateTime StartDate { get; set; }

        public bool AllowNonMembers { get; set; }
        public bool RequirePermission { get; set; }

        public Ruleset Ruleset { get; protected set; }
        public int RulesetId { get; protected set; }

        public IEnumerable<TournamentPlayer> Players { get; protected set; }
        public IEnumerable<Bracket> Brackets { get; protected set; }

        protected Tournament()
        {
        }

        public Tournament(string name, string description, Ruleset ruleset, DateTime startDate)
        {
            SetName(name);
            SetDescription(description);
            SetRuleset(ruleset);

            Status = TournamentStatus.Scheduled;
            StartDate = startDate;

            Players = new List<TournamentPlayer>();
            Brackets = new List<Bracket>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome do torneio não pode ser nulo");

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("A descrição do torneio não pode ser nula");

            Description = description;
        }

        public void SetRuleset(Ruleset ruleset)
        {
            RulesetId = ruleset?.Id ?? throw new ArgumentNullException("O ruleset do torneio não pode ser nulo");
            Ruleset = ruleset;
        }
    }

    public enum TournamentStatus
    {
        Scheduled,
        Running,
        Finished
    }
}