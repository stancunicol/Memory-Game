using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tema_2.ViewModel;
using System.Windows.Input;

namespace Tema_2.View
{
    public partial class MainWindow : Window
    {
        private readonly LoginVM loginVM;

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            loginVM = new LoginVM();

            DataContext = loginVM;

            WindowStyle = WindowStyle.SingleBorderWindow;

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/View/aestheticWallpaper.jpg"));
            Grid.Background = imageBrush;
            Grid.Background.Opacity = 0.4;

            Icon = new BitmapImage(new Uri("pack://application:,,,/View/icon.jpg"));
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(this);
            if (newUserWindow.ShowDialog() == true)
            {
                loginVM.LoadUsernames();
            }
        }

        private void ListBox_Click(object sender, MouseButtonEventArgs e)
        {
            if (((ListBox)sender).SelectedItem is string selectedUser)
            {
                loginVM.SelectedUser =  selectedUser;
            }
        }

        private void Play_Button(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow(this);
            playWindow.Show();
        }
        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}