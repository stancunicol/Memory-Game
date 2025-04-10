using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class NewUserWindow : Window
    {
        readonly NewUserVM newUserVM;

        public NewUserWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            newUserVM = new NewUserVM();

            DataContext = newUserVM;

            WindowStyle = WindowStyle.SingleBorderWindow;

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/View/aestheticWallpaper.jpg"));
            Grid.Background = imageBrush;
            Grid.Background.Opacity = 0.4;

            Icon = new BitmapImage(new Uri("pack://application:,,,/View/icon.jpg"));
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if(newUserVM.IsValid)
            {
                DialogResult = true;
                Close();
            }
        }
    }
}
