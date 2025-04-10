using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.CommandWpf;
using Tema_2.Model;

namespace Tema_2.ViewModel
{
    public class NewUserVM : INotifyPropertyChanged
    {
        private string username;
        private int currentImageIndex;
        private readonly ObservableCollection<string> imagePaths;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool UserCreated = false;

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public BitmapImage CurrentImage
        {
            get
            {
                try
                {
                    var fullPath = Path.GetFullPath(imagePaths[currentImageIndex]);
                    var uri = new Uri(fullPath, UriKind.Absolute);
                    return new BitmapImage(uri);
                }
                catch
                {
                    return new BitmapImage(new Uri(imagePaths[currentImageIndex], UriKind.RelativeOrAbsolute));
                }
            }
        }

        public bool IsValid => !string.IsNullOrEmpty(Username) && (!string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 && Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"));

        public ICommand PreviousImageCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand CreateUserCommand { get; }

        public NewUserVM()
        {
            imagePaths = new ObservableCollection<string>(
                Directory.GetFiles("Avatars", "*.jpg"));

            currentImageIndex = 0;

            PreviousImageCommand = new RelayCommand(
                () => { CurrentImageIndex--; },
                () => CurrentImageIndex > 0);

            NextImageCommand = new RelayCommand(
                () => { CurrentImageIndex++; },
                () => currentImageIndex < imagePaths.Count - 1);

            CreateUserCommand = new RelayCommand(
                () =>
                {
                    var newUser = new User
                    {
                        username = Username,
                        imagePath = imagePaths[currentImageIndex]
                    };
                    var userVM = new UserVM(newUser);
                    userVM.SaveUser();
                },
                () => IsValid);
        }

        public int CurrentImageIndex
        {
            get => currentImageIndex;
            set
            {
                if (currentImageIndex != value)
                {
                    currentImageIndex = value;
                    OnPropertyChanged(nameof(CurrentImageIndex));
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}