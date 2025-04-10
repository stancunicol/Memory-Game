using System.ComponentModel;
using System.Windows;

namespace Tema_2.ViewModel
{
    public class DialogVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int rows = 4;
        public int columns = 4;

        public int Rows
        {
            get => rows;
            set
            {
                if (rows != value && value > 1 && value <= 6)
                {
                    rows = value;
                    OnPropertyChanged(nameof(Rows));
                }
            }
        }
        public int Columns
        {
            get => columns;
            set
            {
                if (columns != value && value > 1 && value <= 6)
                {
                    columns = value;
                    OnPropertyChanged(nameof(Columns));
                }
            }
        }

        public void Validation()
        {
            if ((Rows * Columns) % 2 != 0)
            {
                MessageBox.Show("The number of cards must be even!");
                return;
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
