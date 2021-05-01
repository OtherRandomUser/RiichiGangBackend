namespace RiichiGang.Domain
{
    public class Stats
    {
        public int TotalGames { get; set; }
        public int TotalRounds { get; set; }
        public int FirstPlaces { get; set; }
        public int SecondPlaces { get; set; }
        public int ThirdPlaces { get; set; }
        public int FourthPlaces { get; set; }
        public int TotalBusted { get; set; }
        public int WinRounds { get; set; }
        public int TsumoRounds { get; set; }
        public int CallRounds { get; set; }
        public int RiichiRounds { get; set; }
        public int DealInRounds { get; set; }

        public Stats()
        {
            TotalGames = 0;
            TotalRounds = 0;
            FirstPlaces = 0;
            SecondPlaces = 0;
            ThirdPlaces = 0;
            FourthPlaces = 0;
            TotalBusted = 0;
            WinRounds = 0;
            TsumoRounds = 0;
            CallRounds = 0;
            RiichiRounds = 0;
            DealInRounds = 0;
        }
    }
}