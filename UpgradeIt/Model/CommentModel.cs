using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpgradeIt.Model
{
    public class CommentModel
    {
        public int ProjectId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentBody { get; set; }
    }
}