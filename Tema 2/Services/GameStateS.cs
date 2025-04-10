using System.IO;
using System.Text.Json;
using System.Windows;
using Tema_2.Model;

namespace Tema_2.Services
{
    public class GameStateS
    {
        private GameState CurrentGame { get; set; }

        private string SaveDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

        public GameStateS()
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
        }

        public GameState LoadGame(string username)
        {
            string filePath = GetSaveFilePath(username);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Nu există joc salvat pentru acest utilizator.");

            string jsonString = File.ReadAllText(filePath);
            CurrentGame = JsonSerializer.Deserialize<GameState>(jsonString);
            return CurrentGame;
        }

        public void SaveGame(GameState game)
        {
            if (game == null) return;

            game.LastSaved = DateTime.Now;
            string filePath = GetSaveFilePath(game.Username);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(game, options);
            File.WriteAllText(filePath, jsonString);

            CurrentGame = game;
        }

        public void DeleteSavedGame(string username)
        {
            string filePath = GetSaveFilePath(username);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (CurrentGame != null && CurrentGame.Username == username)
                CurrentGame = null;
        }

        public bool HasSavedGame(string username)
        {
            return File.Exists(GetSaveFilePath(username));
        }

        private string GetSaveFilePath(string username)
        {
            return Path.Combine(SaveDirectory, $"{username}.json");
        }
        public void DeleteUserGame(string username)
        {
            string savesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

            if (!Directory.Exists(savesFolder))
            {
                MessageBox.Show("Directorul Saves nu există.");
                return;
            }

            string filePath = Path.Combine(savesFolder, username + ".json");

            MessageBox.Show($"Căutăm fișierul: {filePath}");

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    MessageBox.Show("Fișierul de joc a fost șters cu succes.");
                }
                else
                {
                    MessageBox.Show("Fișierul de joc nu a fost găsit.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergerea fișierului: {ex.Message}");
            }
        }

    }
}
