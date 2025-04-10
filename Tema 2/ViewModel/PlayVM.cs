using System.ComponentModel;
using System.Windows;
using Tema_2.Model;
using Tema_2.Services;
using Tema_2.View;

namespace Tema_2.ViewModel
{
    public class PlayVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public bool fileButtonClicked { get; set; } = false;
        public bool optionsButtonClicked { get; set; } = false;
        public bool helpButtonClicked { get; set; } = false;
        public bool newGameButtonClicked { get; set; } = false;
        public bool categoryButtonClicked { get; set; } = false;

        public bool IsFileButtonClicked => fileButtonClicked;
        public bool IsOptionsButtonClicked => optionsButtonClicked;
        public bool IsHelpButtonClicked => helpButtonClicked;
        public bool IsNewGameButtonClicked => newGameButtonClicked;
        public bool IsCategoryButtonClicked => categoryButtonClicked;

        public TimeVM timeVM { get; }
        public PlayVM()
        {
            timeVM = new TimeVM();
        }

        public void SetFileButtonClicked(bool value)
        {
            if (fileButtonClicked != value)
            {
                fileButtonClicked = value;
                optionsButtonClicked = !value;
                helpButtonClicked = !value;
                OnPropertyChanged(nameof(IsFileButtonClicked));
                OnPropertyChanged(nameof(IsOptionsButtonClicked));
                OnPropertyChanged(nameof(IsHelpButtonClicked));
            }
        }

        public void SetOptionsButtonClicked(bool value)
        {
            if (optionsButtonClicked != value)
            {
                optionsButtonClicked = value;
                fileButtonClicked = !value;
                helpButtonClicked = !value;
                OnPropertyChanged(nameof(IsOptionsButtonClicked));
                OnPropertyChanged(nameof(IsFileButtonClicked));
                OnPropertyChanged(nameof(IsHelpButtonClicked));
            }
        }

        public void SetHelpButtonClicked(bool value)
        {
            if (helpButtonClicked != value)
            {
                helpButtonClicked = value;
                fileButtonClicked = !value;
                optionsButtonClicked = !value;
                OnPropertyChanged(nameof(IsHelpButtonClicked));
                OnPropertyChanged(nameof(IsFileButtonClicked));
                OnPropertyChanged(nameof(IsOptionsButtonClicked));
            }
        }
        public void SetNewGameButtonClicked(bool value)
        {
            if (newGameButtonClicked != value)
            {
                newGameButtonClicked = value;
                OnPropertyChanged(nameof(IsNewGameButtonClicked));
            }
        }

        public void SetCategoryButtonClicked(bool value)
        {
            if (categoryButtonClicked != value)
            {
                categoryButtonClicked = value;
                OnPropertyChanged(nameof(IsCategoryButtonClicked));
            }
        }

        public void OpenGame()
        {
            string username = Properties.Settings.Default.CurrentUser;
            GameStateS service = new GameStateS();

            if (!service.HasSavedGame(username))
            {
                MessageBox.Show("Nu există niciun joc salvat pentru acest utilizator.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                GameState savedGame = service.LoadGame(username);

                if (savedGame.Type == "Custom")
                {
                    CustomGameWindow customGame = new CustomGameWindow(savedGame); // constructor nou
                    customGame.Show();
                }
                else if (savedGame.Type == "Standard")
                {
                    StandardGameWindow standardGame = new StandardGameWindow(savedGame); // constructor nou
                    standardGame.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la încărcarea jocului: {ex.Message}");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
