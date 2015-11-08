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
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.member;
using umbraco.cms.businesslogic.template;

namespace UpgradeIt.Controllers
{
    public class ProjectEditController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            if (model.Content.TemplateId.Equals(Template.GetTemplateIdFromAlias("ProjectCreate")))
            {
                var member = Member.GetCurrentMember();
                if (member != null)
                {
                    if (Request.HttpMethod.Equals("GET"))
                        return CurrentTemplate(model);

                    var project = CreateProject(member.User);
                    if (project != null) return this.Redirect(umbraco.library.NiceUrl(project.Id));
                }

                return this.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                var id = Convert.ToInt32(Request.Params["id"]);
                var project = new Document(id);

                var member = Member.GetCurrentMember();
                var authorId = project.getProperty("author").Value;
                if (member != null && member.Id.Equals(authorId))
                {
                    if (Request.HttpMethod.Equals("GET"))
                        return base.View(model);

                    SaveProject(project, member.User);
                }

                return this.Redirect(umbraco.library.NiceUrl(id));
            }
        }

        private Document CreateProject(User user)
        {
            if (!string.IsNullOrWhiteSpace(Request["title"]))
            {
                var project = Document.MakeNew(Request["title"], DocumentType.GetByAlias("project"), user, new Node(-1).Id);
                SaveProject(project, user);

                return project;
            }

            return null;
        }

        private void SaveProject(Document project, User user)
        {
            if (!string.IsNullOrWhiteSpace(Request["title"]))
            {
                project.getProperty("title").Value = Request["title"];
            }
            if (!string.IsNullOrWhiteSpace(Request["description"]))
                project.getProperty("description").Value = Request["description"];
            if (!string.IsNullOrWhiteSpace(Request["projectType"]))
                project.getProperty("projectType").Value = Convert.ToInt32(Request["projectType"]);
            if (!string.IsNullOrWhiteSpace(Request["area"]))
                project.getProperty("area").Value = Convert.ToInt32(Request["area"]);
            project.getProperty("allowComments").Value = !string.IsNullOrWhiteSpace(Request["allowComments"]) && Request["allowComments"].ToLower().Equals("on");

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var uploadedFile = Request.Files[0];
                var fileName = Path.GetFileName(uploadedFile.FileName);
                var fileSavePath = Server.MapPath("~/media/projects/" + fileName);
                uploadedFile.SaveAs(fileSavePath);

                project.getProperty("image").Value = "/media/projects/" + fileName;
            }

            project.SaveAndPublish(user);
            umbraco.library.UpdateDocumentCache(project.Id);
        }
    }
}   