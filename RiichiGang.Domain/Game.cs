using System;

namespace RiichiGang.Domain
{
    public class Game : Entity
    {
        public Bracket Bracket { get; protected set; }
        public int BracketId { get; protected set; }

        public int Series { get; set; }
        public BracketPlayer Toncha { get; protected set;}
        public int TonchaId { get; protected set;}
        public BracketPlayer Nancha { get; protected set;}
        public int NanchaId { get; protected set;}
        public BracketPlayer Shacha { get; protected set;}
        public int ShachaId { get; protected set;}
        public BracketPlayer Peicha { get; protected set;}
        public int PeichaId { get; protected set;}

        public DateTime? PlayedAt { get; set; }
        public string LogFile { get; set; }

        public int MatchResultTon { get; set; }
        public float EndScoreTon { get; set; }
        public int MatchResultNan { get; set; }
        public float EndScoreNan { get; set; }
        public int MatchResultSha { get; set; }
        public float EndScoreSha { get; set; }
        public int MatchResultPei { get; set; }
        public float EndScorePei { get; set; }

        protected Game()
        {
        }

        public Game(Bracket bracket, BracketPlayer toncha, BracketPlayer nancha, BracketPlayer shacha, BracketPlayer peicha)
        {
            BracketId = bracket?.Id ?? throw new ArgumentNullException("A chave de um jogo não pode ser nula");
            Bracket = bracket;

            TonchaId = toncha?.Id ?? throw new ArgumentNullException("O toncha não pode ser nulo");
            Toncha = toncha;

            NanchaId = nancha?.Id ?? throw new ArgumentNullException("O nancha não pode ser nulo");
            Nancha = nancha;

            ShachaId = shacha?.Id ?? throw new ArgumentNullException("O shacha não pode ser nulo");
            Shacha = shacha;

            PeichaId = peicha?.Id ?? throw new ArgumentNullException("O peicha não pode ser nulo");
            Peicha = peicha;

            PlayedAt = null;
        }
    }
}