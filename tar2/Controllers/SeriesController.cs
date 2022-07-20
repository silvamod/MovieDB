using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tar2.Models.DAL;

namespace tar2.Controllers
{
    public class SeriesController : ApiController
    {
        // GET api/<controller>
        public List<Series> Get()
        {
            Series s = new Series();
            List<Series> sList = s.Get();
            return sList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]Series series)
        {
            series.Insert();
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