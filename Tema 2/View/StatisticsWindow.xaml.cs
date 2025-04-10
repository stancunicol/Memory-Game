using System.Windows;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class StatisticsWindow : Window
    {
        StatisticsVM statisticsVM;
        public StatisticsWindow(Window owner)
        {
            InitializeComponent();
            statisticsVM = new StatisticsVM();
            DataContext = statisticsVM;

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
