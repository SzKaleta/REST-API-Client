using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestApi.Models.ServerModels;
using RestApi.Models.ClientModels;
using RestApi.Models;
using Newtonsoft.Json;

namespace RestApi.Controllers
{
    public class ClientController : Controller
    {
        public ActionResult Options()
        {
            return View();
        }
        private async Task<List<Users>> GetUsers()
        {
           using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55279/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = await client.GetStringAsync("api/Server");
                var list = JsonConvert.DeserializeObject<List<Users>>(json);
                return list;
            }
        }

        private async Task<UserModel> GetUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55279/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = await client.GetStringAsync("api/Server/"+id.ToString());
                var result = JsonConvert.DeserializeObject<UserModel>(json);
                return result;
            }
        }

        public async Task<ActionResult> AllUsers()
        {
            GetAllModel listOfUsers = new GetAllModel();
            listOfUsers.results= await GetUsers();
            if(!listOfUsers.results.Any())
            {
                return RedirectToAction("Error", "Error", new { type = "The list of users is empty" });
            }
            else
            {
                return View(listOfUsers);
            }
        }

        public async Task<ActionResult> OneUser(int id)
        {
            UserModel user = await GetUser(id);
            if (user.user_id==0)
            {
                return RedirectToAction("Error", "Error", new { type = "No user with this id exists in the database" });
            }
            else
            {
                return View(user);
            }
        }

        public async Task<ActionResult> UpdateUser(string name, int? age, int id)
        {
            using (var client = new HttpClient())
            {
                UserModel user = new UserModel();
                if (!String.IsNullOrEmpty(name))
                {
                    user.name = name;
                }
                if (age > 0 && age < 121)
                {
                    user.age = age;
                }
                client.BaseAddress = new Uri("http://localhost:55279/");
                HttpResponseMessage messege = await client.PutAsJsonAsync("api/Server/" + id.ToString(), user);
                var content = await messege.Content.ReadAsStringAsync();

                if (messege.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllUsers", "Client");
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { type = "Update unsuccessful" });
                }
            }
        }

        public async Task<ActionResult> AddUser(string name, int? age)
        {   
            HttpResponseMessage messege = new HttpResponseMessage();
            if (!String.IsNullOrEmpty(name) && (age==null || (age > 0 && age < 121)))
            {
                using (var client = new HttpClient())
                {
                    UserModel user = new UserModel();
                    if(age!=null)
                    {
                        user.age = age;
                    }
                    user.name = name;
                    client.BaseAddress = new Uri("http://localhost:55279/");
                    messege = await client.PostAsJsonAsync("api/Server", user);
                }
            }
            if (messege.IsSuccessStatusCode)
            {
                return RedirectToAction("AllUsers", "Client");
            }
            else
            {
                return RedirectToAction("Error", "Error", new { type = "Insert unsuccessful" });
            }
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55279/");
                HttpResponseMessage messege = await client.DeleteAsync("api/Server/" + id.ToString());
                if (messege.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllUsers", "Client");
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { type = "Delete unsuccessful" });
                }
            }
        }       
    }
}
