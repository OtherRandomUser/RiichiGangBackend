using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class Bracket : Entity
    {
        public Tournament Tournament { get; protected set; }
        public int TournamentId { get; protected set; }

        public int Sequence { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string Name { get; protected set; }
        public WinCondition WinCondition { get; protected set; }
        public int NumberOfAdvancing { get; protected set; }
        public int NumberOfSeries { get; protected set; }
        public int GamesPerSeries { get; protected set; }
        public float FinalScoreMultiplier { get; protected set; }

        public IEnumerable<BracketPlayer> Players { get; protected set; }
        public IEnumerable<Series> Series { get; protected set; }

        protected Bracket()
        {
        }

        public Bracket(
            Tournament tournament,
            string name,
            int sequence,
            WinCondition winCondition,
            int numberOfAdvancing,
            int numberOfSeries,
            int gamesPerSeries)
        {
            TournamentId = tournament?.Id ?? throw new ArgumentNullException("O torneio de uma chave não pode ser nulo");
            Tournament = tournament;

            if (sequence < 0)
                throw new ArgumentException("A sequência deve ser maior que zero");

            SetName(name);
            SetWinCondition(winCondition, numberOfAdvancing);
            SetStructure(numberOfSeries, gamesPerSeries);

            Players = new List<BracketPlayer>();
            Series = new List<Series>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome de uma chave não pode ser nulo");

            Name = name;
        }

        public void SetWinCondition(WinCondition winCondition, int numberOfAdvancing)
        {
            if (winCondition == WinCondition.TopX && numberOfAdvancing <= 0)
                throw new ArgumentException("uma chave do tipo \"TopX\" precisa de um número de jogadores para a próxima fase maior do que zero");

            WinCondition = winCondition;

            if (winCondition == WinCondition.TopX)
                NumberOfAdvancing = numberOfAdvancing;
        }

        public void SetStructure(int numberOfSeries, int gamesPerSeries)
        {
            if (numberOfSeries <= 0)
                throw new ArgumentException("O número de séries por jogador deve ser maior que zero");

            if (gamesPerSeries <= 0)
                throw new ArgumentException("O número de jogos por série deve ser maior que zero");

            NumberOfSeries = numberOfSeries;
            GamesPerSeries = gamesPerSeries;
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