    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tar2.Models.DAL;

namespace tar2.Models
{
    public class Episode
    {
        string name, nameOfEp, picture, description;
        int numOfSeason,id,seriesId;
        string date;
        int likeCount;

        public Episode()
        {

        }

        public Episode(int id,int seriesId,string name, string nameOfEp, string picture, string description, int numOfSeason, string date)
        {
            this.id = id;
            this.seriesId = seriesId;
            this.name = name;
            this.nameOfEp = nameOfEp;
            this.picture = picture;
            this.description = description;
            this.numOfSeason = numOfSeason;
            this.date = date;
        }

        public string Name { get => name; set => name = value; }
        public string NameOfEp { get => nameOfEp; set => nameOfEp = value; }
        public string Picture { get => picture; set => picture = value; }
        public string Description { get => description; set => description = value; }
        public int NumOfSeason { get => numOfSeason; set => numOfSeason = value; }
        public string Date { get => date; set => date = value; }
        public int Id { get => id; set => id = value; }
        public int SeriesId { get => seriesId; set => seriesId = value; }
        public int LikeCount { get => likeCount; set => likeCount = value; }

        public List<Episode> Get()
        {
            DataServices ds = new DataServices();
            return ds.GetEpisodes();
        }

        public List<Episode> Get(int id)
        {
            DataServices ds = new DataServices();
            List<Episode> eList = ds.Get(id);
            return eList;
        }

        public List<Episode> Get(string tvName,int id)
        {
            DataServices ds = new DataServices();
            List<Episode> eList = ds.Get(tvName,id);
            return eList;
        }

        public int Insert(int id)
        {
            DataServices ds = new DataServices();
            return ds.Insert(this,id);
            
        }

    }
}