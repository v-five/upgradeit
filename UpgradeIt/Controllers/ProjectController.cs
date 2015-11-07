using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace UpgradeIt.Controllers
{
    public class ProjectEditController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var id = Convert.ToInt32(Request.Params["id"]);
            if (umbraco.BusinessLogic.User.GetCurrent() != null)
            {
                if(Request.HttpMethod.Equals("GET"))
                    return base.View(model);

                var project = new Document(id);

                project.getProperty("title").Value = Request["title"];
                project.getProperty("description").Value = Request["description"];
                project.getProperty("image").Value = Request["image"];
                project.getProperty("allowComments").Value = Request["allowComments"];
                project.getProperty("projectType").Value = Request["projectType"];
                project.getProperty("projectArea").Value = Request["projectArea"];

                project.SaveAndPublish(umbraco.BusinessLogic.User.GetCurrent());
            }

            return this.Redirect(umbraco.library.NiceUrl(id));
        }
    }
}   