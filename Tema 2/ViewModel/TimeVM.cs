using System.ComponentModel;

namespace Tema_2.ViewModel
{
    public class TimeVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string selectedHour = "0";
        private string selectedMinute = "0";
        private string selectedSecond = "0";

        public string SelectedHours
        {
            get => selectedHour;
            set
            {
                if (selectedHour != value)
                {
                    selectedHour = value;
                    OnPropertyChanged(nameof(SelectedHours));
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
        public string SelectedMinutes
        {
            get => selectedMinute;
            set
            {
                if (selectedMinute != value)
                {
                    selectedMinute = value;
                    OnPropertyChanged(nameof(SelectedMinutes));
                }
            }
        }
        public string SelectedSeconds
        {
            get => selectedSecond;
            set
            {
                if (selectedSecond != value)
                {
                    selectedSecond = value;
                    OnPropertyChanged(nameof(SelectedSeconds));
                }
            }
        }
        public TimeSpan SelectedTime => new TimeSpan(
        int.Parse(SelectedHours),
        int.Parse(SelectedMinutes),
        int.Parse(SelectedSeconds));

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
