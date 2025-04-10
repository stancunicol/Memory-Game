using System.Windows;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class DialogWindow : Window
    {
        DialogVM dialogVM;
        TimeVM time;
        public DialogWindow(Window owner, TimeVM timeVM)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            dialogVM = new DialogVM();
            DataContext = dialogVM;
            time = timeVM;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            dialogVM.Validation();
            this.Close();
            CustomGameWindow customGameWindow = new CustomGameWindow(dialogVM.Rows, dialogVM.Columns, time);
            customGameWindow.Show();
        }
    }
}
