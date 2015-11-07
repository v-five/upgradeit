using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace UpgradeIt.Model
{
    public class JsonProjectModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ProjectUrl { get; set; }

        public JsonProjectModel(IPublishedProperty title, IPublishedProperty description, IPublishedProperty image, string url)
        {
            this.Title = title != null ? title.Value.ToString() : "";
            this.Description = description != null ? description.Value.ToString() : "";
            this.ImageUrl = image != null ? image.Value.ToString() : "";
            this.ProjectUrl = url;
        }
    }
}