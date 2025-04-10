using System.Windows;
using Tema_2.Model;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class StandardGameWindow : Window
    {
        private StandardGameVM standardGameVM;
        public StandardGameWindow(Window owner, TimeVM timeVM)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            standardGameVM = new StandardGameVM(timeVM);
            DataContext = standardGameVM;

            standardGameVM.GameCompleted += OnGameCompleted;
            standardGameVM.GameFailed += OnGameFailed;
            standardGameVM.GameSaved += OnGameSaved;
        }
        public StandardGameWindow(GameState state)
        {
            InitializeComponent();
            standardGameVM = new StandardGameVM(state);
            DataContext = standardGameVM;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            standardGameVM.GameCompleted += OnGameCompleted;
            standardGameVM.GameFailed += OnGameFailed;
            standardGameVM.GameSaved += OnGameSaved;
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
