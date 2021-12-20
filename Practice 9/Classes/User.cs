using System;
using Newtonsoft.Json;

namespace Practice_9.Classes
{
    public class User
    {
        public Role role;
        public string Name;
        public DateTime Birthday;
        public DateTime regDate;
        public int salary;
        public string login;
        public string password;
        public string email;
        public string phone;
        [JsonConstructor]
        public User(string name, DateTime birthday,Role role,string login,string pass,string email,string phone)
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