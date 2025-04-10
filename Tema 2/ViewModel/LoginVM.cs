using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tema_2.Services;

namespace Tema_2.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        public ObservableCollection<UserVM> users { get; } = new ObservableCollection<UserVM>();
        private readonly UserS userS = new UserS();
        public List<string> usernames { get; set;  } = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        private string selectedUser;
        private BitmapImage selectedUserImage;
        private ICommand deleteUserCommand;

        public LoginVM() 
        {
            LoadUsernames();
        }

        public void LoadUsernames()
        {
            users.Clear();
            usernames = new List<string>();

            foreach(var user in userS.LoadUsers())
            {
                var userVM = new UserVM(user);
                users.Add(userVM);
                usernames.Add(userVM.Username);
            }

            OnPropertyChanged(nameof(usernames));
        }

        public string SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                SelectedUserImage = GetUserImage(value);
                Properties.Settings.Default.CurrentUser = selectedUser;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(IsAUserSelected));
                Properties.Settings.Default.Save();
            }
        }

        public BitmapImage SelectedUserImage
        {
            get => selectedUserImage;
            private set
            {
                selectedUserImage = value;
                OnPropertyChanged(nameof(SelectedUserImage));
            }
        }

        private BitmapImage GetUserImage(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                var fullPath = Path.GetFullPath(user.ImagePath);
                var uri = new Uri(fullPath, UriKind.Absolute);
                return new BitmapImage(uri);
            }
            return null;
        }

        public ICommand DeleteUserCommand => deleteUserCommand ??= new RelayCommand(
            () => DeleteSelectedUser(),
            () => !string.IsNullOrEmpty(SelectedUser)
            );

        private void DeleteSelectedUser()
        {
            if (string.IsNullOrEmpty(SelectedUser))
                return;

            var userToDelete = users.FirstOrDefault(u => u.Username == SelectedUser);

            if (userToDelete != null)
            {
                GameStateS service = new GameStateS();
                service.DeleteUserGame(SelectedUser);
                userS.DeleteUser(userToDelete.Username);

                LoadUsernames();

                SelectedUser = null;
                SelectedUserImage = null;
                Properties.Settings.Default.CurrentUser = "";
                Properties.Settings.Default.Save();
            }
        }

        public bool IsAUserSelected => selectedUser != null;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
