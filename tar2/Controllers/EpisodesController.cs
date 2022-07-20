using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tar2.Models;

namespace tar2.Controllers
{
    public class EpisodesController : ApiController
    {
        //int -> string sesaon ->tvname
        public IEnumerable<Episode> Get(string tvName,int id)
        {
            Episode e = new Episode();
            List<Episode> eList = e.Get(tvName,id);
            return eList;
        }
        //// GET api/<controller>
        public IEnumerable<Episode> Get(int id)
        {
            Episode e = new Episode();
            List<Episode> eList = e.Get(id);
            return eList;
        }
        //this is method to give us all the data from episodes db
        public List<Episode> Get()
        {
            Episode s = new Episode();
            List<Episode> sList = s.Get();
            return sList;
        }

        ////return change , string -> int
        //// POST api/<controller>
        public int Post([FromBody] Episode episode,int id)
        {
            return episode.Insert(id);
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}