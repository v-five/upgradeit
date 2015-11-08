using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UpgradeIt.Model;

namespace UpgradeIt.Controllers
{
    public class SearchController : SurfaceController
    {
        // GET: Search
        public ActionResult Index()
        {
            string searchKeyword = "";
            if (Request.Params.AllKeys.Contains("keyword"))
            {
                searchKeyword = Request.Params["keyword"].ToString();
            }

            var rootNode = Umbraco.TypedContentAtRoot().First();

            var searchResults = rootNode.Children.Where(p => p.GetProperty("isApproved") != null && (bool)p.GetProperty("isApproved").Value && p.GetProperty("title")
                .Value.ToString().ToLower().Contains(searchKeyword.ToLower())).Select(x=>new JsonProjectModel(x.GetProperty("title"), x.GetProperty("description"), x.GetProperty("image"), x.Url));

            Response.AddHeader("Access-Control-Allow-Origin", "*");

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }
    }
}