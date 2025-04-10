using System.ComponentModel;
using Tema_2.Model;

namespace Tema_2.ViewModel
{
    public class CardVM : INotifyPropertyChanged
    {
        private Card card = new Card();
        public string CurrentImage => IsFlipped ? ImagePath : BackImagePath;
        public event PropertyChangedEventHandler PropertyChanged;

        public string ImagePath
        {
            get => card.imagePath;
            set
            {
                if (card.imagePath != value)
                {
                    card.imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                    OnPropertyChanged(nameof(CurrentImage)); // Adăugat
                }
            }
        }

        public string BackImagePath
        {
            get => card.backImagePath;
            set
            {
                if (card.backImagePath != value)
                {
                    card.backImagePath = value;
                    OnPropertyChanged(nameof(BackImagePath));
                    OnPropertyChanged(nameof(CurrentImage)); // Adăugat
                }
            }
        }

        public bool IsFlipped
        {
            get => card.isFlipped;
            set
            {
                if (card.isFlipped != value)
                {
                    card.isFlipped = value;
                    OnPropertyChanged(nameof(IsFlipped));
                    OnPropertyChanged(nameof(CurrentImage)); // Adăugat
                }
            }
        }

        public bool IsMatched
        {
            get => card.isMatched;
            set
            {
                if (card.isMatched != value)
                {
                    card.isMatched = value;
                    OnPropertyChanged(nameof(IsMatched));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
