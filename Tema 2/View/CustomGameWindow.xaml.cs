using System.Windows;
using Tema_2.Model;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class CustomGameWindow : Window
    {
        private CustomGameVM customGameVM;
        public CustomGameWindow(int rows, int columns, TimeVM timeVM)
        {

            InitializeComponent();

            customGameVM = new CustomGameVM(rows, columns, timeVM);
            DataContext = customGameVM;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;


            customGameVM.GameCompleted += OnGameCompleted;
            customGameVM.GameFailed += OnGameFailed;
            customGameVM.GameSaved += OnGameSaved;
        }
        public CustomGameWindow(GameState state)
        {
            InitializeComponent();
            customGameVM = new CustomGameVM(state);
            DataContext = customGameVM;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            customGameVM.GameCompleted += OnGameCompleted;
            customGameVM.GameFailed += OnGameFailed;
            customGameVM.GameSaved += OnGameSaved;
        }


        private void OnGameCompleted(object sender, EventArgs e)
        {
            this.Close();

            MessageBox.Show("Felicitări! Ai completat jocul!", "Joc terminat",
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OnGameFailed(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("Ai pierdut jocul!", "Joc terminat",
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OnGameSaved(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
