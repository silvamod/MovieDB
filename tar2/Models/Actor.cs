using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tar2.Models
{

    public class Actor
    {
        int id, gender;
        string name, popularity,picture;
        public Actor()
        {

        }
        public Actor(int id, int gender, string name, string character, string popularity,string picture)
        {
            this.id = id;
            this.gender = gender;
            this.name = name;
            this.popularity = popularity;
            this.picture = picture;
        }

        public int Id { get => id; set => id = value; }
        public int Gender { get => gender; set => gender = value; }
        public string Name { get => name; set => name = value; }
        public string Popularity { get => popularity; set => popularity = value; }
        public string Picture { get => picture; set => picture = value; }

        public int Insert(int id)
        {
            DataServices ds = new DataServices();
            return ds.Insert(this,id);
        }
    }
}