namespace RiichiGang.Service.InputModel
{
    public class BracketInputModel
    {
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string WinCondition { get; set; }
        public int NumberOfAdvancing { get; set; }
        public int NumberOfSeries { get; set; }
        public int GamesPerSeries { get; set; }
        public float FinalScoreMultiplier { get; set; }
    }
}