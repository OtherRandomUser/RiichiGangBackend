using System;
using System.Collections.Generic;
using System.Linq;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class BracketViewModel
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string StartedAt { get; set; }
        public string FinishedAt { get; set; }
        public int Sequence { get; set; }
        public string Description { get; set; }
        public IEnumerable<SeriesViewModel> Series { get; set; }

        public BracketViewModel(Bracket bracket, Tournament tournament)
        {
            if (bracket is null)
                throw new ArgumentNullException(nameof(bracket));

            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            var winCon = "";

            switch (bracket.WinCondition)
            {
                case Domain.WinCondition.First:
                    winCon = "Apenas o primeiro colocado de cada série irá avançar para a próxima chave.";
                    break;

                case Domain.WinCondition.FirstAndSecond:
                    winCon = "O primeiro e o segundo colocado de cada série irão avançar para a próxima chave.";
                    break;

                case Domain.WinCondition.TopX:
                    winCon = $"Após o final da chave, os {bracket.NumberOfAdvancing} jogadores com a melhor pontuação irão avançar para a próxima chave.";
                    break;

                case Domain.WinCondition.None:
                    winCon = "Chave final do torneio.";
                    break;
            }

            var gameDescr = "Essa chave consiste de ";

            if (bracket.NumberOfSeries == 1)
            {
                gameDescr += "uma série por jogador";
            }
            else
            {
                gameDescr += $"{bracket.NumberOfSeries} séries por jogador";
            }

            gameDescr += ", cada série composta de ";

            if (bracket.GamesPerSeries == 1)
            {
                gameDescr += "apenas um jogo.";
            }
            else
            {
                gameDescr += $"{bracket.GamesPerSeries} jogos.";
            }

            Id = bracket.Id;
            TournamentId = bracket.TournamentId;
            OwnerId = tournament.Club.OwnerId;
            Name = bracket.Name;
            CreatedAt = bracket.CreatedAt.ToString("dd/MM/yyyy");
            StartedAt = bracket.StartedAt?.ToString("dd/MM/yyyy") ?? "";
            FinishedAt = bracket.FinishedAt?.ToString("dd/MM/yyyy") ?? "";
            Sequence = bracket.Sequence;
            Description = $"{winCon} {gameDescr}";
            Series = bracket.Series.Select(s => new SeriesViewModel(s, bracket.GamesPerSeries));
        }
    }
}