using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class Bracket : Entity
    {
        public Tournament Tournament { get; protected set; }
        public int TournamentId { get; protected set; }

        public int Sequence { get; set; }
        public string Name { get; protected set; }
        public WinCondition WinCondition { get; set; }
        public int NumberOfAdvancing { get; set; }
        public int NumberOfSeries { get; set; }
        public int GamesPerSeries { get; set; }
        public float FinalScoreMultiplier { get; set; }

        public IEnumerable<BracketPlayer> Players { get; protected set; }
        public IEnumerable<Game> Games { get; protected set; }

        protected Bracket()
        {
        }

        public Bracket(Tournament tournament, string name)
        {
            TournamentId = tournament?.Id ?? throw new ArgumentNullException("O torneio de uma chave não pode ser nulo");
            Tournament = tournament;

            SetName(name);

            Players = new List<BracketPlayer>();
            Games = new List<Game>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome de uma chave não pode ser nulo");

            Name = name;
        }
    }

    public enum WinCondition
    {
        First,
        FirstAndSecond,
        TopX,
        None
    }
}