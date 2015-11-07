using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.media;
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
            var project = new Document(id);

            var member = umbraco.cms.businesslogic.member.Member.GetCurrentMember();
            var authorId = project.getProperty("author").Value;
            if (member != null && member.Id.Equals(authorId))
            {
                if(Request.HttpMethod.Equals("GET"))
                    return base.View(model);

                if (!string.IsNullOrWhiteSpace(Request["title"]))
                    project.getProperty("title").Value = Request["title"];
                if (!string.IsNullOrWhiteSpace(Request["description"]))
                    project.getProperty("description").Value = Request["description"];
                if (!string.IsNullOrWhiteSpace(Request["projectType"]))
                    project.getProperty("projectType").Value = Request["projectType"];
                if (!string.IsNullOrWhiteSpace(Request["area"]))
                    project.getProperty("area").Value = Convert.ToInt32(Request["area"]);
                project.getProperty("allowComments").Value = !string.IsNullOrWhiteSpace(Request["allowComments"]) && Request["allowComments"].ToLower().Equals("on");

                if (Request.Files.Count > 0)
                {
                    var uploadedFile = Request.Files[0];
                    var fileName = Path.GetFileName(uploadedFile.FileName);
                    var fileSavePath = Server.MapPath("~/media/projects/" + fileName);
                    uploadedFile.SaveAs(fileSavePath);

                    project.getProperty("image").Value = "/media/projects/" + fileName;
                }

                project.SaveAndPublish(new umbraco.BusinessLogic.User(0));
                umbraco.library.UpdateDocumentCache(id);
            }

            return this.Redirect(umbraco.library.NiceUrl(id));
        }
    }
}   