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
                FirstRate = Math.Round(((double) stats.FirstPlaces / stats.TotalGames) * 100, 2),
                SecondRate = Math.Round(((double) stats.SecondPlaces / stats.TotalGames) * 100, 2),
                ThirdRate = Math.Round(((double) stats.ThirdPlaces / stats.TotalGames) * 100, 2),
                FourthRate = Math.Round(((double) stats.FourthPlaces / stats.TotalGames) * 100, 2),
                BustingRate = Math.Round(((double) stats.TotalBusted / stats.TotalGames) * 100, 2),
                WinRate = Math.Round(((double) stats.WinRounds / stats.TotalRounds) * 100, 2),
                TsumoRate = Math.Round(((double) stats.TsumoRounds / stats.TotalRounds) * 100, 2),
                CallRate = Math.Round(((double) stats.CallRounds / stats.TotalRounds) * 100, 2),
                RiichiRate = Math.Round(((double) stats.RiichiRounds / stats.TotalRounds) * 100, 2),
                DealInRate = Math.Round(((double) stats.DealInRounds / stats.TotalRounds) * 100, 2),
            };
        }
    }
}