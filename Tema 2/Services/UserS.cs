using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Tema_2.Model;

namespace Tema_2.Services
{
    internal class UserS
    {
        private readonly string filePath = "Users.json";

        public List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void AddUser(User user)
        {
            var users = LoadUsers();

            if (users.Any(u => u.username == user.username))
            {
                MessageBox.Show("Username already exits.");
                return;
            }

            users.Add(user);
            SaveUsers(users);
        }

        public void DeleteUser(string username)
        {
            var users = LoadUsers();
            var userToDelete = users.FirstOrDefault(u => u.username == username);

            if (userToDelete != null)
            {
                if (!string.IsNullOrEmpty(userToDelete.imagePath) &&
                    File.Exists(userToDelete.imagePath))
                {
                    try
                    {
                        File.Delete(userToDelete.imagePath);
                    }
                    catch {}
                }

                users.Remove(userToDelete);
                SaveUsers(users);
            }
        }
    }
}