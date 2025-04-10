using Tema_2.ViewModel;

namespace Tema_2.Model
{
    public class GameState
    {
        public string Username { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public int MatchedPairsCount { get; set; }
        public List<CardVM> Cards { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Category { get; set; }
        public DateTime LastSaved { get; set; } = DateTime.Now;
        public string Type { get; set; }

        public GameState(string username, TimeSpan remainingTime, int matchedPairsCount, List<CardVM> cards, int rows, int columns, string category, string type)
        {
            Username = username;
            RemainingTime = remainingTime;
            MatchedPairsCount = matchedPairsCount;
            Cards = cards;
            Rows = rows;
            Columns = columns;
            Category = category;
            Type = type;
        }
    }
}
