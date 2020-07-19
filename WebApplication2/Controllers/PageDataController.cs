using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Web.UI;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication2.Controllers
{
    public class PageDataController : Controller
    {
        // GET: PageData
        [HttpPost]
        public ActionResult Save(string txtPage)
        {
            var url = "";
            if (txtPage != "")
            {
                url = "https://reqres.in/api/example?per_page=2&page=" + txtPage + "";
            }
            else
            {
                url = "https://reqres.in/api/example?per_page=2&page=1";
            }
            TempData["url"] = url;
            return RedirectToAction("PageData");
        }
        [HttpGet]
        public ActionResult PageData()
        {

            // return View();
            var model = TempData["url"] as string;
            List<PageData> organizations = new List<PageData>();
            
           // var json2="";
            string text = "";
         
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var url = "";
                    webClient.Encoding = Encoding.UTF8;
                    if (model == ""||model==null)
                    {
                        url = "https://reqres.in/api/example?per_page=2&page=1";
                    }

                    else
                    {
                        url = model;
                    }
                    var json2 = webClient.DownloadString(url);
                   
                    PageData[] arr = JObject.Parse(json2)["data"].ToObject<PageData[]>();
                   
                    foreach (PageData dt in arr)
                    {
                        string val = dt.pantone_value;
                        string stringAfterChar = val.Substring(0,val.IndexOf("-"));
                        
                        if (int.Parse(stringAfterChar) % 3==0)
                        {
                            dt.group = "Group1";
                        }
                        else if (int.Parse(stringAfterChar) % 2 == 0)
                        {
                            dt.group = "Group2";
                        }
                        else
                        {
                            dt.group="Group3";
                        }

                    }
                        
                        ViewBag.data = arr;
                    }
                }
            
            catch (Exception e)
            {
                text = "error";
            }
           
           
            return View();

        }

       
    }

}