using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace UpgradeIt.Controllers
{
    public class LikeController : SurfaceController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                int projectId = 0;
                if (Request.Params.AllKeys.Contains("projectId"))
                {
                    projectId = Convert.ToInt32(Request.Params["projectId"].ToString());
                }


                IEnumerable<Node> allLikes = uQuery.GetNodesByType("Like");

                var memberService = ApplicationContext.Services.MemberService;
                var contentService = ApplicationContext.Services.ContentService;
                var currentUserId = memberService.GetByUsername(User.Identity.Name).Id;

                var projectToLike = contentService.GetById(projectId);

                var projectLikesFolder = projectToLike.Children().Where(c => c.ContentType.Alias.Equals("Likes")).FirstOrDefault();

                if (projectLikesFolder == null)
                {
                    projectLikesFolder = contentService.CreateContent("Likes", projectToLike, "Likes");
                    contentService.Publish(projectLikesFolder);
                }

                var alreadyLiked = projectLikesFolder.Children().Where(x => x.GetValue("author") != null && x.GetValue("author").ToString().Equals(currentUserId.ToString())).FirstOrDefault();

                if (alreadyLiked == null)
                {
                    var createdLike = contentService.CreateContent("Like", projectLikesFolder, "Like");
                    createdLike.SetValue("author", currentUserId);

                    contentService.Publish(createdLike);
                }
                else
                {
                    contentService.Delete(alreadyLiked);
                }

            }


            return Json(new 
            { 
                success = true, 
                errorMessage = "" 
            }, JsonRequestBehavior.AllowGet);
        }
    }
}