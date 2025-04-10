using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using Tema_2.Model;
using Tema_2.Services;

namespace Tema_2.ViewModel
{
    public class StandardGameVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string category;
        private const int pairs = 8;
        private readonly Random random = new Random();
        private List<CardVM> images = new List<CardVM>();
        private List<CardVM> allAvailableImages = new List<CardVM>();
        private CardVM firstSelectedCard;
        private bool canSelect;
        private int matchedPairsCount;
        public event EventHandler GameCompleted;
        public event EventHandler GameFailed;
        private User user = new User();
        private UserS usersS = new UserS();
        public TimeSpan remainingTime;
        public TimeVM timeVM;
        public DispatcherTimer timer;
        public event EventHandler GameSaved;
        private ICommand _saveGameCommand;
        public ICommand SaveGameCommand => _saveGameCommand ??= new GalaSoft.MvvmLight.Command.RelayCommand(SaveGame);
        public bool AllPairsMatched => MatchedPairsCount == pairs;
        public TimeSpan RemainingTime
        {
            get => remainingTime;
            set
            {
                remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }

        public int MatchedPairsCount
        {
            get => matchedPairsCount;
            set
            {
                matchedPairsCount = value;
                OnPropertyChanged(nameof(MatchedPairsCount));
                OnPropertyChanged(nameof(AllPairsMatched)); // Notifică schimbarea
            }
        }

        public StandardGameVM(TimeVM timeVM)
        {
            this.timeVM = timeVM;
            RemainingTime = timeVM.SelectedTime;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += OnTimerTick;
            category = Properties.Settings.Default.ChosenCategory;
            InitializeGame();
            SelectPairs();
            ShuffleCards();
            canSelect = true;
            MatchedPairsCount = 0;
            timer.Start();
        }
        public StandardGameVM(GameState state)
        {
            this.timeVM = new TimeVM
            {
                SelectedHours = state.RemainingTime.Hours.ToString(),
                SelectedMinutes = state.RemainingTime.Minutes.ToString(),
                SelectedSeconds = state.RemainingTime.Seconds.ToString()
            };

            RemainingTime = state.RemainingTime;
            category = state.Category;
            Images = state.Cards;
            matchedPairsCount = state.MatchedPairsCount;

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += OnTimerTick;
            canSelect = true;
            timer.Start();
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (RemainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                OnGameFailed();
                MessageBox.Show("Timpul a expirat! Ai pierdut.");
                return;
            }

            RemainingTime = RemainingTime.Subtract(TimeSpan.FromSeconds(1));
            OnPropertyChanged(nameof(RemainingTime));
        }

        public List<CardVM> Images
        {
            get => images;
            set
            {
                images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectPairs()
        {
            images.Clear();

            var selectedImages = allAvailableImages
                .OrderBy(x => random.Next())
                .Take(pairs)
                .ToList();

            foreach (var image in selectedImages)
            {
                images.Add(new CardVM
                {
                    ImagePath = image.ImagePath,
                    BackImagePath = image.BackImagePath
                });
                images.Add(new CardVM
                {
                    ImagePath = image.ImagePath,
                    BackImagePath = image.BackImagePath
                });
            }

            OnPropertyChanged(nameof(Images));
        }

        private void InitializeGame()
        {
            allAvailableImages.Clear();
            images.Clear();
            if (category == "flowers")
                LoadAllFlowerImages();
            else if (category == "cats")
                LoadAllCatsImages();
            else if (category == "shells")
                LoadAllShellsImages();
            OnPropertyChanged(nameof(Images));
        }

        private void LoadAllFlowerImages()
        {
            for (int i = 1; i <= 18; i++)
            {
                string imagePath = $"pack://application:,,,/View/Flowers/flower{i}.jpg";
                string backImagePath = "pack://application:,,,/View/backImage.jpg";
                allAvailableImages.Add(new CardVM { ImagePath = imagePath, BackImagePath = backImagePath });
            }
        }
        private void LoadAllCatsImages()
        {
            for (int i = 1; i <= 18; i++)
            {
                string imagePath = $"pack://application:,,,/View/Cats/cat{i}.jpg";
                string backImagePath = "pack://application:,,,/View/backImage.jpg";
                allAvailableImages.Add(new CardVM { ImagePath = imagePath, BackImagePath = backImagePath });
            }
        }
        private void LoadAllShellsImages()
        {
            for (int i = 1; i <= 18; i++)
            {
                string imagePath = $"pack://application:,,,/View/Shells/shell{i}.jpg";
                string backImagePath = "pack://application:,,,/View/backImage.jpg";
                allAvailableImages.Add(new CardVM { ImagePath = imagePath, BackImagePath = backImagePath });
            }
        }
        public void ShuffleCards()
        {
            images = images.OrderBy(x => random.Next()).ToList();
            OnPropertyChanged(nameof(Images));
        }

        private ICommand _imageClickCommand;
        public ICommand ImageClickCommand => _imageClickCommand ?? (_imageClickCommand = new RelayCommand<CardVM>(OnImageClick));

        public void OnImageClick(CardVM card)
        {
            if (card.IsFlipped || card.IsMatched || !canSelect)
                return;

            card.IsFlipped = true;
            OnPropertyChanged(nameof(Images));

            if (firstSelectedCard == null)
            {
                firstSelectedCard = card;
            }
            else
            {
                canSelect = false;
                if (firstSelectedCard.ImagePath == card.ImagePath)
                {
                    firstSelectedCard.IsMatched = true;
                    card.IsMatched = true;
                    MatchedPairsCount++;
                    firstSelectedCard = null;
                    canSelect = true;
                    if (AllPairsMatched)
                    {
                        timer.Stop();
                        OnGameCompleted();
                    }
                }
                else
                {
                    Task.Delay(1000).ContinueWith(t =>
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            firstSelectedCard.IsFlipped = false;
                            card.IsFlipped = false;
                            firstSelectedCard = null;
                            canSelect = true;
                        });
                    });
                }
            }
        }
        private void AddWinnedGame()
        {
            string username = Properties.Settings.Default.CurrentUser;
            var users = usersS.LoadUsers();

            var existingUser = users.First(u => u.username == username);

            existingUser.winnedGames++;
            existingUser.standardGames++;
            existingUser.gamesPlayed++;
            usersS.SaveUsers(users);
            OnPropertyChanged(nameof(user));
            OnPropertyChanged(nameof(usersS));
        }

        private void AddLoosedGame()
        {
            string username = Properties.Settings.Default.CurrentUser;
            var users = usersS.LoadUsers();

            var existingUser = users.First(u => u.username == username);

            existingUser.loosedGames++;
            existingUser.standardGames++;
            existingUser.gamesPlayed++;
            usersS.SaveUsers(users);
            OnPropertyChanged(nameof(user));
            OnPropertyChanged(nameof(usersS));
        }

        private void OnGameCompleted()
        {
            AddWinnedGame();
            GameStateS service = new GameStateS();
            string username = Properties.Settings.Default.CurrentUser;
            service.DeleteSavedGame(username);
            GameCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void OnGameFailed()
        {
            AddLoosedGame();
            GameStateS service = new GameStateS();
            string username = Properties.Settings.Default.CurrentUser;
            service.DeleteSavedGame(username);
            GameCompleted?.Invoke(this, EventArgs.Empty);
        }
        public void SaveGame()
        {
            string username = Properties.Settings.Default.CurrentUser;

            GameState gameState = new GameState(
                username,
                RemainingTime,
                MatchedPairsCount,
                Images,
                4,
                4,
                category,
                "Standard"
            );

            timer.Stop();

            GameStateS service = new GameStateS();
            service.SaveGame(gameState);

            MessageBox.Show("Jocul a fost salvat cu succes.", "Salvare",
                            MessageBoxButton.OK, MessageBoxImage.Information);
            GameSaved?.Invoke(this, EventArgs.Empty);
        }
    }
}
