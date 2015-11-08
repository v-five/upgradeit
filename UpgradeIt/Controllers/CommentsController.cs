using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using UpgradeIt.Model;

namespace UpgradeIt.Controllers
{
    public class CommentsController : UmbracoApiController
    {
        [HttpPost]
        public void AddComment([FromBody] CommentModel comment)
        {
            if (User.Identity.IsAuthenticated && !String.IsNullOrEmpty(comment.CommentBody))
            {
                var memberService = ApplicationContext.Services.MemberService;
                var contentService = ApplicationContext.Services.ContentService;
                var currentUserId = memberService.GetByUsername(User.Identity.Name).Id;

                var projectToComment = contentService.GetById(comment.ProjectId);

                var projectCommentsFolder = contentService.GetChildren(projectToComment.Id).Where(c => c.ContentType.Alias.Equals("Comments")).FirstOrDefault();

                if (projectCommentsFolder == null)
                {
                    projectCommentsFolder = contentService.CreateContent("Comments", projectToComment, "Comments");
                    contentService.Publish(projectCommentsFolder);
                }

                var createdComment = contentService.CreateContent(String.IsNullOrEmpty(comment.CommentTitle) ? "Comment" : comment.CommentTitle, projectCommentsFolder, "Comment");
                createdComment.SetValue("author", currentUserId);
                createdComment.SetValue("title", comment.CommentTitle);
                createdComment.SetValue("text", comment.CommentBody);

                contentService.Publish(createdComment);
            }

        }
    }
}