using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using RestApi.Models;
using RestApi.Models.ServerModels;


namespace RestApi.Controllers
{
    public class ServerController : ApiController
    {
        public UsersEntitiesData baseContext { get; set; }

        public ServerController()
        {
            baseContext = new UsersEntitiesData();
        }

        public ServerController(UsersEntitiesData newDb)
        {
            baseContext = newDb;
        }

        // GET: api/Server
        public List<UserModel> Get()
        {
                var UsersList = new List<UserModel>(from a in baseContext.Users.AsEnumerable()
                                                    select new UserModel
                                                    {
                                                        user_id = a.user_id,
                                                        name = a.name,
                                                        age=a.age
                                                    }).ToList();
                return UsersList;
        }

        // GET: api/Server/5
        public UserModel Get(int id)
        {
                var UserInstance = new UserModel();
                    UserInstance.user_id = baseContext.Users.Where(d => d.user_id==id).Select(d => d.user_id).FirstOrDefault();
                    UserInstance.name = baseContext.Users.Where(d => d.user_id == id).Select(d => d.name).FirstOrDefault();
                    UserInstance.age = baseContext.Users.Where(d => d.user_id == id).Select(d => d.age).FirstOrDefault();
                return UserInstance;
        }

        // POST: api/Server
        public void Post([FromBody]UserModel newUser)
        {
            if(newUser!=null && (!String.IsNullOrEmpty(newUser.name) &&(newUser.age==null||(newUser.age>0&& newUser.age<121))))
            {
                var insertUser = new Users
                {     
                    name = newUser.name,
                    age = newUser.age
                };
                    baseContext.Users.Add(insertUser);
                    baseContext.SaveChanges();
            }
        }

        // PUT: api/Server/5
        public void Put(int id, [FromBody]UserModel changeUser)
        {
            var result = ReturnById(id);
            if (result != null)
            {
                if (changeUser.age > 0 && changeUser.age < 121)
                {
                    result.age = changeUser.age;
                }
                if (!String.IsNullOrEmpty(changeUser.name))
                {
                    result.name = changeUser.name;
                }
                baseContext.SaveChanges();
            }
        }


        // DELETE: api/Server/5
        public void Delete(int id)
        {
           
            var deleted = ReturnById(id);
            if (deleted != null)
            {
                baseContext.Users.Remove(deleted);
                baseContext.SaveChanges();
            }
        }

        protected virtual Users ReturnById(int id)
        {
           return baseContext.Users.SingleOrDefault(d => d.user_id == id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (baseContext != null)
                {
                    baseContext.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
