using System;

namespace RiichiGang.Domain
{
    public class Ruleset : Entity
    {
        public string Name { get; protected set; }

        public Club Club { get; protected set; }
        public int ClubId { get; protected set; }

        public int Mochiten { get; set; }
        public int Genten { get; set; }
        public int UmaFirst { get; set; }
        public int UmaSecond { get; set; }
        public int UmaThird { get; set; }
        public int UmaFourth { get; set; }
        public int Oka { get; set; }
        public bool Atozuke { get; set; }
        public bool Kuitan { get; set; }
        public Kuikae Kuikae { get; set; }
        public bool UraDora { get; set; }
        public bool KanDora { get; set; }
        public bool KanUraDora { get; set; }
        public bool AkaDora { get; set; }
        public bool AgariYame { get; set; }
        public bool TenpaiYame { get; set; }
        public bool Tobi { get; set; }

        protected Ruleset()
        {
        }

        public Ruleset(Club club, string name)
        {
            ClubId = club?.Id ?? throw new ArgumentNullException(
                "O clube dono de um ruleset não pode ser nulo");
            Club = club;

            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome do ruleset não pode ser nulo");

            Name = name;
        }

        public static Ruleset WRC(Club club)
        {
            return new Ruleset
            {
                Name = "World Riichi Championship Rules",
                ClubId = club?.Id ?? throw new ArgumentNullException(
                    "O clube dono de um ruleset não pode ser nulo"),
                Club = club,
                Mochiten = 30000,
                Genten = 30000,
                UmaFirst = 15,
                UmaSecond = 5,
                UmaThird = -5,
                UmaFourth = -15,
                Oka = 0,
                Atozuke = true,
                Kuitan = true,
                Kuikae = Kuikae.Forbidden,
                UraDora = true,
                KanDora = true,
                KanUraDora = true,
                AkaDora = false,
                AgariYame = false,
                TenpaiYame = false,
                Tobi = false,
            };
        }

        public static Ruleset ML(Club club)
        {
            return new Ruleset
            {
                Name = "M-League Rules",
                ClubId = club?.Id ?? throw new ArgumentNullException(
                    "O clube dono de um ruleset não pode ser nulo"),
                Club = club,
                Mochiten = 25000,
                Genten = 30000,
                UmaFirst = 30,
                UmaSecond = 10,
                UmaThird = -10,
                UmaFourth = -30,
                Oka = 20,
                Atozuke = true,
                Kuitan = true,
                Kuikae = Kuikae.Forbidden,
                UraDora = true,
                KanDora = true,
                KanUraDora = true,
                AkaDora = true,
                AgariYame = false,
                TenpaiYame = false,
                Tobi = false,
            };
        }
    }

    public enum Kuikae
    {
        Allowed,
        PartlyAllowed,
        Forbidden
    }
}