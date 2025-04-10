using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using Tema_2.Model;
using Tema_2.Services;

namespace Tema_2.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        private User user;
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapImage userAvatar;
        private UserS userS;

        public string Username
        {
            get => user.username;
            set
            {
                if (user.username != value)
                {
                    user.username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string ImagePath
        {
            get => user.imagePath;
            set
            {
                if (user.imagePath != value)
                {
                    user.imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                    LoadUserAvatar();
                }
            }
        }

        public BitmapImage UserAvatar
        {
            get => userAvatar;
            private set
            {
                userAvatar = value;
                OnPropertyChanged(nameof(UserAvatar));
            }
        }
        public int WinnedGames
        {
            get => user.winnedGames;
            set
            {
                user.winnedGames = value;
                OnPropertyChanged(nameof(WinnedGames));
            }
        }
        public int LoosedGames
        {
            get => user.loosedGames;
            set
            {
                user.loosedGames = value;
                OnPropertyChanged(nameof(LoosedGames));
            }
        }

        public int PlayedGames
        {
            get => user.gamesPlayed;
            set
            {
                user.gamesPlayed = value;
                OnPropertyChanged(nameof(PlayedGames));
            }
        }
        public int StandardGames
        {
            get => user.standardGames;
            set
            {
                user.standardGames = value;
                OnPropertyChanged(nameof(StandardGames));
            }
        }
        public int CustomGames
        {
            get => user.customGames;
            set
            {
                user.customGames = value;
                OnPropertyChanged(nameof(CustomGames));
            }
        }

        private void LoadUserAvatar()
        {
            if (string.IsNullOrEmpty(ImagePath))
            {
                UserAvatar = null;
                return;
            }

            try
            {
                var uri = new Uri(ImagePath, UriKind.RelativeOrAbsolute);
                UserAvatar = new BitmapImage(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Eroare la încărcarea avatarului: {ex.Message}");
                UserAvatar = null;
            }
        }

        public UserVM() 
        {
            userS= new UserS();
            user = new User();
        }


        public UserVM(User user)
        {
            userS = new UserS();
            this.user = user ?? new User();
            LoadUserAvatar();
        }

        public void SaveUser()
        {
            userS.AddUser(user);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}