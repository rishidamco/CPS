using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using CPSWebSite.Models;
using System.Linq;

namespace CPSWebSite.Controllers
{
    public class CityController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://localhost:5418/api/City/";//Please change port no while running on your system

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter

        public ActionResult CityList()
        {
            return View();
        }

        public ActionResult UpdateCityList(string id)
        {
            var cityDetail = new List<cityListModel>();
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //calling WebApi Method on passing zip code
            HttpResponseMessage response = client.GetAsync("GetCityByID/" + id).Result;  // Blocking call!
            // on Success return result set
            if (response.IsSuccessStatusCode)
            {
                cityDetail = response.Content.ReadAsAsync<IEnumerable<cityListModel>>().Result.ToList();
            }
            return View(cityDetail);
        }
    }
}
