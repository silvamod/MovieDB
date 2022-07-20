using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tar2.Models.DAL;

namespace tar2.Models
{
    public class User
    {
        string name, lastName, mail, password, phoneNum, sex, yearOfBirth, favGenre, address;
        int id;
        bool isAdmin;
        public User()
        {
        }
        public User(string name, string lastName, string mail, string password, string phoneNum, string sex, string yearOfBirth, string favGenre, string address)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Mail = mail;
            this.Password = password;
            this.PhoneNum = phoneNum;
            this.Sex = sex;
            this.YearOfBirth = yearOfBirth;
            this.FavGenre = favGenre;
            this.Address = address;
            this.IsAdmin = false;
        }

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Sex { get => sex; set => sex = value; }
        public string YearOfBirth { get => yearOfBirth; set => yearOfBirth = value; }
        public string FavGenre { get => favGenre; set => favGenre = value; }
        public string Address { get => address; set => address = value; }
        public int Id { get => id; set => id = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        public List<User> Get()
        {
            DataServices ds = new DataServices();
            return ds.Get();
        }

        public User Get(String user, String pass)
        {
            DataServices ds = new DataServices();
            return ds.Get(user,pass);
        }
        public int Insert()
        {
            DataServices ds = new DataServices();
            ds.Insert(this);
            return 1;
        }

    }
}