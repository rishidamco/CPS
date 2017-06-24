using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Linq;
using CPSWebAPI.Models;
using System.Web.Script.Serialization; // for serialize and deserialize 
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace CPSWebAPI.Controllers
{
    public class CityController : ApiController
    {
        public List<cityListModel> GetCityByName(string name)
        {
            //get the Json filepath  
            string file = HttpContext.Current.Server.MapPath("~/Scripts/zips.json");
            //deserialize JSON from file  
            string Json = File.ReadAllText(file);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //Set max value of Json string
            ser.MaxJsonLength = Int32.MaxValue;
            //deserialize JSON file and mapping to model properties  
            List<cityListModel> cityList = ser.Deserialize<List<cityListModel>>(Json);
            //filter desired list on matching string in city name  
            cityList = cityList.Where(a => a.city.ToUpper().Contains(name.ToUpper())).ToList();
            return cityList;
        }

        public List<cityListModel> GetCityByID(string id)
        {
            //get the Json filepath  
            string file = HttpContext.Current.Server.MapPath("~/Scripts/zips.json");
            //deserialize JSON from file  
            string Json = File.ReadAllText(file);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //Set max value of Json string
            ser.MaxJsonLength = Int32.MaxValue;
            //deserialize JSON file and mapping to model properties  
            List<cityListModel> cityList = ser.Deserialize<List<cityListModel>>(Json);
            //filter desired list on matching Zip Code  
            cityList = cityList.Where(a => a._id == id).ToList();
            return cityList;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage updateCityPopulation(cityListModel cityDetail)
        {
            string zipCode = cityDetail._id;
            Int64 pop = cityDetail.pop;
            string file = HttpContext.Current.Server.MapPath("~/Scripts/zips.json");
            string json = File.ReadAllText(file);
            List<cityListModel> cityList = JsonConvert.DeserializeObject<List<cityListModel>>(json);
            foreach (var item in cityList.Where(w => w._id == zipCode))
            {
                item.pop = pop;
            }
            string output = JsonConvert.SerializeObject(cityList, Formatting.Indented);
            File.WriteAllText(file, output);
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
    }
}
