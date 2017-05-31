using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using MovieShopXS.Models;
using Newtonsoft.Json;
using MovieShopXSLib.ViewModels;
namespace MovieShopXS.Controllers
{
    


    [Route("")]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        HttpClient client;
        public MovieController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2018");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            List<MovieVM> data = getData();
            return View(data);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Add()
        {
           
            return View(new Movie() );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                List<MovieVM> data = getData();
                return View("index", data);
            }

           
            return View(movie);
        }


        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("/api/movie"+id).Result;
            List<MovieVM> data = getData();

            return View("index",data);
        }

        private List<MovieVM> getData()
        {
            HttpResponseMessage response = client.GetAsync("/api/movie").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<MovieVM> data = JsonConvert.DeserializeObject<List<MovieVM>>(stringData);
            return data;
        }
    }
}
