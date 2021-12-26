using System;
using Newtonsoft.Json;

namespace Practice_9.Classes
{
    public class User
    {
        public DateTime Birthday;
        public string email;
        public string login;
        public string Name;
        public string password;
        public string phone;
        public DateTime regDate;
        public Role role;

        [JsonConstructor]
        public User(string name, DateTime birthday, Role role, string login, string pass, string email, string phone)
        {
            Name = name;
            Birthday = birthday;
            this.role = role;
            this.login = login;
            password = pass;
            this.email = email;
            this.phone = phone;
            regDate = DateTime.Today;
        }
    }
}