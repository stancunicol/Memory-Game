namespace Tema_2.Model
{
    public class User
    {
        public string username {  get; set; }
        public string imagePath { get; set; }
        public int gamesPlayed { get; set; }
        public int loosedGames { get; set; }
        public int winnedGames { get; set; }
        public int standardGames { get; set; }
        public int customGames { get; set; }    
        public string category { get; set; }

        public User(string username, string imagePath, int gamesPlayed, int loosedGames, int winnedGames, int standardGames, int customGames, string category)
        {
            this.username = username;
            this.imagePath = imagePath;
            this.gamesPlayed = gamesPlayed;
            this.loosedGames = loosedGames;
            this.winnedGames = winnedGames;
            this.standardGames = standardGames;
            this.customGames = customGames;
            this.category = category;
        }

        public User()
        {
            username = string.Empty;
            imagePath = string.Empty;
            gamesPlayed = 0;
            loosedGames = 0;
            winnedGames = 0;
            standardGames = 0;
            customGames = 0;
            category = string.Empty;
        }

        public User(string username, int winnedGames, int standardGames, int customGames, int loosedGames, int gamesPlayed)
        {
            this.username = username;
            this.winnedGames = winnedGames;
            this.standardGames = standardGames;
            this.customGames = customGames;
            this.loosedGames = loosedGames;
            this.gamesPlayed = gamesPlayed;
        }
    }
}