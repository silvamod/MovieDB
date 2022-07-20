using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tar2.Models;

namespace tar2.Controllers
{
    public class UserController : ApiController
    {
        public List<User> Get()
        {
            User u = new User();
            List<User> uList = u.Get();
            return uList;
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(String user,String pass)
        {
            User u = new User();
            u = u.Get(user,pass);
            if (u != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, u);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Incorrect Username or Password.");
        }

        // POST api/<controller>
        public int Post([FromBody]User user)
        {
            user.Insert();
            return 1;
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