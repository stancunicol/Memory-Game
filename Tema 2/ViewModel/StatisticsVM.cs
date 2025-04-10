using System.ComponentModel;
using System.Text;
using Tema_2.Services;

namespace Tema_2.ViewModel
{
    public class StatisticsVM : INotifyPropertyChanged
    {
        private List<UserVM> users = new List<UserVM>();
        private UserS userS = new UserS();

        public string StatsText { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public StatisticsVM()
        {
            LoadStatistics();
        }
        public void LoadStatistics()
        {
            var users = userS.LoadUsers();
            var sb = new StringBuilder();

            sb.AppendLine("STATISTICI JUCĂTORI");
            sb.AppendLine("===================");

            foreach (var user in users)
            {
                sb.AppendLine($"Utilizator: {user.username}");
                sb.AppendLine($"- Total jocuri: {user.gamesPlayed}");
                sb.AppendLine($"- Jocuri standard: {user.standardGames}");
                sb.AppendLine($"- Jocuri custom: {user.customGames}");
                sb.AppendLine($"- Jocuri câștigate: {user.winnedGames}");
                sb.AppendLine($"- Jocuri pierdute: {user.loosedGames}");
                sb.AppendLine();
            }

            StatsText = sb.ToString();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
