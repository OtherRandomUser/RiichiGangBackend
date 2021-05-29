using System;

namespace RiichiGang.Domain
{
    public class Game : Entity
    {
        public Series Series { get; set; }
        public int SeriesId { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Player3 { get; set; }
        public Player Player4 { get; set; }

        public DateTime? PlayedAt { get; set; }
        public string LogLink { get; set; }
        public string LogFile { get; set; }

        protected Game()
        {
        }

        public Game(Series series)
        {
            SeriesId = series?.Id ?? throw new ArgumentNullException("A série de um jogo não pode ser nula");
            Series = series;
            Player1 = new Player();
            Player2 = new Player();
            Player3 = new Player();
            Player4 = new Player();
            PlayedAt = null;
        }
    }

    public class Player
    {
        public Seat Seat { get; set; }
        public int MatchResult { get; set; }
        public float EndScore { get; set; }
        public float RunningTotal { get; set; }
    }

    public enum Seat
    {
        East,
        South,
        West,
        North
    }
}