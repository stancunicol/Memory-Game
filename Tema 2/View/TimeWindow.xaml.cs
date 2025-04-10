using System.Windows;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class TimeWindow : Window
    {
        TimeVM timeVM;
        public TimeWindow(Window owner, TimeVM timeVm)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            timeVM = timeVm;
            DataContext = timeVM;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Selected time: {timeVM.SelectedTime}");
            this.Close();
        }
    }
}
