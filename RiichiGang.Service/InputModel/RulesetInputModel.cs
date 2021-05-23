namespace RiichiGang.Service.InputModel
{
    public class RulesetInputModel
    {
        public string Name { get; set; }
        public int Mochiten { get; set; }
        public int Genten { get; set; }
        public int UmaFirst { get; set; }
        public int UmaSecond { get; set; }
        public int UmaThird { get; set; }
        public int UmaFourth { get; set; }
        public int Oka { get; set; }
        public bool Atozuke { get; set; }
        public bool Kuitan { get; set; }
        public string Kuikae { get; set; }
        public bool UraDora { get; set; }
        public bool KanDora { get; set; }
        public bool KanUraDora { get; set; }
        public bool AkaDora { get; set; }
        public bool AgariYame { get; set; }
        public bool TenpaiYame { get; set; }
        public bool Tobi { get; set; }
    }
}