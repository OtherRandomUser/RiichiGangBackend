using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class StatsViewModel
    {
        public int TotalGames { get; set; }
        public int TotalRounds { get; set; }
        public double FirstRate { get; set; }
        public double SecondRate { get; set; }
        public double ThirdRate { get; set; }
        public double FourthRate { get; set; }
        public double BustingRate { get; set; }
        public double WinRate { get; set; }
        public double TsumoRate { get; set; }
        public double CallRate { get; set; }
        public double RiichiRate { get; set; }
        public double DealInRate { get; set; }

        public static implicit operator StatsViewModel(Stats stats)
        {
            if (stats is null)
                return null;

            return new StatsViewModel
            {
                TotalGames = stats.TotalGames,
                TotalRounds = stats.TotalRounds,
                FirstRate = stats.TotalGames == 0 ? 0 : Math.Round(((double) stats.FirstPlaces / stats.TotalGames) * 100, 2),
                SecondRate = stats.TotalGames == 0 ? 0 : Math.Round(((double) stats.SecondPlaces / stats.TotalGames) * 100, 2),
                ThirdRate = stats.TotalGames == 0 ? 0 : Math.Round(((double) stats.ThirdPlaces / stats.TotalGames) * 100, 2),
                FourthRate = stats.TotalGames == 0 ? 0 : Math.Round(((double) stats.FourthPlaces / stats.TotalGames) * 100, 2),
                BustingRate = stats.TotalGames == 0 ? 0 : Math.Round(((double) stats.TotalBusted / stats.TotalGames) * 100, 2),
                WinRate = stats.TotalRounds == 0 ? 0 : Math.Round(((double) stats.WinRounds / stats.TotalRounds) * 100, 2),
                TsumoRate = stats.TotalRounds == 0 ? 0 : Math.Round(((double) stats.TsumoRounds / stats.TotalRounds) * 100, 2),
                CallRate = stats.TotalRounds == 0 ? 0 : Math.Round(((double) stats.CallRounds / stats.TotalRounds) * 100, 2),
                RiichiRate = stats.TotalRounds == 0 ? 0 : Math.Round(((double) stats.RiichiRounds / stats.TotalRounds) * 100, 2),
                DealInRate = stats.TotalRounds == 0 ? 0 : Math.Round(((double) stats.DealInRounds / stats.TotalRounds) * 100, 2),
            };
        }
    }
}