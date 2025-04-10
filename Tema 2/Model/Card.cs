
namespace Tema_2.Model
{
    public class Card
    {
        public string imagePath { get; set; }
        public string backImagePath { get; set; }
        public bool isFlipped { get; set; }
        public bool isMatched { get; set; }

        public Card()
        {
            imagePath = string.Empty;
            backImagePath = string.Empty;
            isFlipped = false;
            isMatched = false;
        }

        public Card(string imagePath, string backImagePath, bool isFlipped, bool isMatched)
        {
            this.imagePath = imagePath;
            this.backImagePath = backImagePath;
            this.isFlipped = isFlipped;
            this.isMatched = isMatched;
        }

        public Card(string imagePath, string backImagePath)
        {
            this.imagePath = imagePath;
            this.backImagePath = backImagePath;
            isFlipped = false;
            isMatched = false;
        }
    }
}
