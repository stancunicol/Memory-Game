using System.Windows;
using Tema_2.ViewModel;

namespace Tema_2.View
{
    public partial class CategoryWindow : Window
    {
        CategoryVM categoryVM;
        public CategoryWindow(Window owner)
        {
            InitializeComponent();

            categoryVM = new CategoryVM();

            DataContext = categoryVM;

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
        private void FlowersButtonClick(object sender, RoutedEventArgs e)
        {
            categoryVM.FlowersClicked();
            this.Close();
        }
        private void CatsButtonClick(object sender, RoutedEventArgs e)
        {
            categoryVM.CatsClicked();
            this.Close();
        }
        private void ShellsButtonClick(object sender, RoutedEventArgs e)
        {
            categoryVM.ShellsClicked();
            this.Close();
        }
    }
}
