using System;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.ViewModel
{
    public class RulesetViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int Mochiten { get; set; }
        public int Genten { get; set; }
        public string Uma { get; set; }
        public int Oka { get; set; }
        public string Atozuke { get; set; }
        public string Kuitan { get; set; }
        public string Kuikae { get; set; }
        public string UraDora { get; set; }
        public string KanDora { get; set; }
        public string KanUraDora { get; set; }
        public string AkaDora { get; set; }
        public string AgariYame { get; set; }
        public string TenpaiYame { get; set; }
        public string Tobi { get; set; }

        public static implicit operator RulesetViewModel(Ruleset ruleset)
        {
            if (ruleset is null)
                return null;

            var uma = $"{ruleset.UmaFirst}/{ruleset.UmaSecond}/{ruleset.UmaThird}/{ruleset.UmaFourth}";
            var kuikae  = default(string);

            switch (ruleset.Kuikae)
            {
                case Domain.Kuikae.Allowed:
                    kuikae = "Permitido";
                    break;

                case Domain.Kuikae.PartlyAllowed:
                    kuikae = "Parcialmente Permitido";
                    break;

                case Domain.Kuikae.Forbidden:
                    kuikae = "Proibido";
                    break;
            }

            return new RulesetViewModel
            {
                Id = ruleset.Id,
                CreatedAt = ruleset.CreatedAt,
                Name = ruleset.Name,
                Mochiten = ruleset.Mochiten,
                Genten = ruleset.Genten,
                Uma = uma,
                Oka = ruleset.Oka,
                Atozuke = ruleset.Atozuke ? "Sim" : "Não",
                Kuitan = ruleset.Kuitan ? "Sim" : "Não",
                Kuikae = kuikae,
                UraDora = ruleset.UraDora ? "Sim" : "Não",
                KanDora = ruleset.KanDora ? "Sim" : "Não",
                KanUraDora = ruleset.KanUraDora ? "Sim" : "Não",
                AkaDora = ruleset.AkaDora ? "Sim" : "Não",
                AgariYame = ruleset.AgariYame ? "Sim" : "Não",
                TenpaiYame = ruleset.TenpaiYame ? "Sim" : "Não",
                Tobi = ruleset.Tobi ? "Sim" : "Não"
            };
        }
    }
}