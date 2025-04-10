using System.Windows;
using Tema_2.Model;
using Tema_2.Services;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class PlayWindow : Window
    {
        readonly PlayVM playVM;
        public PlayWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            playVM = new PlayVM();
            DataContext = playVM;
        }
        private void FileClick(object sender, RoutedEventArgs e)
        {
            playVM.SetFileButtonClicked(true);
        }
        private void OptionsClick(object sender, RoutedEventArgs e)
        {
            playVM.SetOptionsButtonClicked(true);
        }
        private void HelpClick(object sender, RoutedEventArgs e)
        {
            playVM.SetHelpButtonClicked(true);
        }
        private void CategoryClick(object sender, RoutedEventArgs e)
        {
            playVM.SetCategoryButtonClicked(true);

            CategoryWindow categoryWindow = new CategoryWindow(this);
            categoryWindow.Show();
        }
        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            playVM.SetNewGameButtonClicked(true);

            TimeWindow timeWindow = new TimeWindow(this, playVM.timeVM);
            timeWindow.Show();
        }
        private void StandardClick(object sender, RoutedEventArgs e)
        {
            StandardGameWindow standardGameWindow = new StandardGameWindow(this, playVM.timeVM);
            standardGameWindow.Show();
        }
        private void CustomClick(object sender, RoutedEventArgs e)
        {
            DialogWindow dialogWindow = new DialogWindow(this, playVM.timeVM);
            dialogWindow.Show();
        }
        private void StatisticsClick(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(this);
            statisticsWindow.Show();
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow(this);
            aboutWindow.Show();
        }
        private void OpenGameClick(object sender, RoutedEventArgs e)
        {
            playVM.OpenGame();
        }

    }
}
