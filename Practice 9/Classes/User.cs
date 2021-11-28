using System;

namespace Practice_9.Classes
{
    public class User
    {
        public Role role;
        public string Name;
        public DateTime Birthday;
        public int salary;
        public string login;
        public string password;
        public string email;
        public string phone;
        public User(string name, DateTime birthday,Role role,string login,string pass)
        {
            Name = name;
            Birthday = birthday;
            this.role = role;
            this.login = login;
            password = pass;
        }
        public User(string login,string pass,string email,string phone)
        {
            
            this.role = Role.CLIENT;
            this.login = login;
            password = pass;
            this.email = email;
            this.phone = phone;
        }   


    }
}