using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using umbraco;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using UpgradeIt.Model;

namespace UpgradeIt.Controllers
{
    public class LikeController : UmbracoApiController
    {
        [HttpPost]
        public void AddLike([FromBody] LikeModel like)
        {
            if (User.Identity.IsAuthenticated)
            {
                var memberService = ApplicationContext.Services.MemberService;
                var contentService = ApplicationContext.Services.ContentService;
                var currentUserId = memberService.GetByUsername(User.Identity.Name).Id;

                var projectToLike = contentService.GetById(like.ProjectId);

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
        }
    }
}