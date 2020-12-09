using PokedexAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id_user = 1, username = "ash", password = "pikachu", role = "trainer" });
            users.Add(new User { Id_user = 2, username = "misty", password = "togepi", role = "gymleader" });
            return users.Where(x => x.username.ToLower() == username.ToLower() && x.password == x.password).FirstOrDefault();
        }

    }
}
