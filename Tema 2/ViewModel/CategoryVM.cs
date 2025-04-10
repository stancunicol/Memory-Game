using System.ComponentModel;
using Tema_2.Model;
using Tema_2.Services;

namespace Tema_2.ViewModel
{
    public class CategoryVM : INotifyPropertyChanged
    {
        private User user = new User();
        private UserS usersS = new UserS();
        public event PropertyChangedEventHandler PropertyChanged;
        public string category {  get; set; }

        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public void FlowersClicked()
        {
            category = "flowers";
            OnPropertyChanged(Category);
            SelectCategoryForCurrentUser();
        }
        public void CatsClicked()
        {
            category = "cats";
            OnPropertyChanged(Category);
            SelectCategoryForCurrentUser();
        }
        public void ShellsClicked()
        {
            category = "shells";
            OnPropertyChanged(Category);
            SelectCategoryForCurrentUser();
        }
        private void SelectCategoryForCurrentUser()
        {
            string username = Properties.Settings.Default.CurrentUser;
            var users = usersS.LoadUsers();

            var existingUser = users.First(u => u.username == username);
            existingUser.category = category;
            Properties.Settings.Default.ChosenCategory = category;
            Properties.Settings.Default.Save();
            usersS.SaveUsers(users);
            OnPropertyChanged(nameof(user));
            OnPropertyChanged(nameof(usersS));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
