using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestApi.Models;
using RestApi.Models.ServerModels;


namespace RestApi.Controllers
{
    public class ServerController : ApiController
    {
        private UsersEntitiesData db;

        public ServerController()
        {
            db = new UsersEntitiesData();
        }

        public ServerController(UsersEntitiesData newDb)
        {
            db = newDb;
        }


        // GET: api/Server
        public List<UserModel> Get()
        {


                var UsersList = new List<UserModel>(from a in db.Users.AsEnumerable()

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
                    UserInstance.user_id = db.Users.Where(d => d.user_id==id).Select(d => d.user_id).FirstOrDefault();
                    UserInstance.name = db.Users.Where(d => d.user_id == id).Select(d => d.name).FirstOrDefault();
                    UserInstance.age = db.Users.Where(d => d.user_id == id).Select(d => d.age).FirstOrDefault();
                return UserInstance;

        }

        // POST: api/Server
        public void Post([FromBody]UserModel newUser)
        {
            if(newUser!=null && !String.IsNullOrEmpty(newUser.name) && (newUser.age>0&& newUser.age<121))
            {
                var insertUser = new Users
                {
                        
                        name = newUser.name,
                        age = newUser.age
                    };
                    db.Users.Add(insertUser);
                    db.SaveChanges();
            }

            

        }

        // PUT: api/Server/5
        public void Put(int id, [FromBody]UserModel changeUser)
        {

                    var result = db.Users.SingleOrDefault(d => d.user_id == id);
                if (result != null)
                {
                    if (changeUser.age != null)
                    {
                        result.age = changeUser.age;
                    }

                    if (!String.IsNullOrEmpty(changeUser.name))
                    {
                        result.name = changeUser.name;
                    }

                    db.SaveChanges();
                }
            
        }

        // DELETE: api/Server/5
        public void Delete(int id)
        {

                var deleted = db.Users.SingleOrDefault(d => d.user_id == id);
                if (deleted != null)
                {
                db.Users.Remove(deleted);
                db.SaveChanges();
                }

            
        }
    }
}
