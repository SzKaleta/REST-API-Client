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
        public async Task<List<Users>> GetUsers()
        {
           using (var client = new HttpClient())
            {
                string i = "5";
                client.BaseAddress = new Uri("http://localhost:55279/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = await client.GetStringAsync("api/Server"+i);
                var list = JsonConvert.DeserializeObject<List<Users>>(json);
                return list;
            }
        }

        public async Task<ActionResult> AllUsers()
        {
            GetAllModel listOfUsers = new GetAllModel();
            listOfUsers.results= await GetUsers();
            return View(listOfUsers);
        }

        public ActionResult Options()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
