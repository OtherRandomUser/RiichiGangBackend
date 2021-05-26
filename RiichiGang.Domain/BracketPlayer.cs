using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class BracketPlayer : Entity
    {
        public TournamentPlayer Player { get; protected set; }
        public int PlayerId { get; protected set; }

        public Bracket Bracket { get; protected set; }
        public int BracketId { get; protected set; }

        public float Score { get; set; }

        protected BracketPlayer()
        {
        }

        public BracketPlayer(TournamentPlayer player, Bracket bracket)
        {
            PlayerId = player?.Id ?? throw new ArgumentNullException("O jogador de uma chave não pode ser nulo");
            Player = player;

            BracketId = bracket?.Id ?? throw new ArgumentNullException("A chave não pode ser nula");
            Bracket = bracket;

            Score = 0;
        }
    }
}