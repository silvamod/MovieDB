using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tar2.Models;

namespace tar2.Controllers
{
    public class ActorController : ApiController
    {

        // GET api/<controller>/5
        public List<Actor> Get(int id)
        {
            DataServices ds = new DataServices();
            List<Actor> eList = ds.GetActors(id);
            return eList;
        }

        // POST api/<controller>
        public int Post([FromBody]Actor actor,int id)
        {
            return actor.Insert(id);
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