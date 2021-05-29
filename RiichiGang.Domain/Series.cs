using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class Series : Entity
    {
        public Bracket Bracket { get; protected set; }
        public int BracketId { get; protected set; }

        public BracketPlayer Player1 { get; protected set; }
        public int Player1Id { get; protected set; }

        public BracketPlayer Player2 { get; protected set; }
        public int Player2Id { get; protected set; }

        public BracketPlayer Player3 { get; protected set; }
        public int Player3Id { get; protected set; }

        public BracketPlayer Player4 { get; protected set; }
        public int Player4Id { get; protected set; }

        public IEnumerable<Game> Games { get; protected set; }

        protected Series()
        {
        }

        public Series(Bracket bracket, BracketPlayer player1, BracketPlayer player2, BracketPlayer player3, BracketPlayer player4)
        {
            BracketId = bracket?.Id ?? throw new ArgumentNullException("A chave de um jogo não pode ser nula");
            Bracket = bracket;

            Player1Id = player1?.Id ?? throw new ArgumentNullException("O Jogador 1 não pode ser nulo");
            Player1 = player1;

            Player2Id = player2?.Id ?? throw new ArgumentNullException("O Jogador 2 não pode ser nulo");
            Player2 = player2;

            Player3Id = player3?.Id ?? throw new ArgumentNullException("O Jogador 3 não pode ser nulo");
            Player3 = player3;

            Player4Id = player4?.Id ?? throw new ArgumentNullException("O Jogador 4 não pode ser nulo");
            Player4 = player4;
        }
    }
}