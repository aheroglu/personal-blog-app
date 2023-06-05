using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Entity.Concrete
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int ClickCount { get; set; }
        public bool Status { get; set; }    

        public int PostCategoryId { get; set; }
        public PostCategory PostCategory { get; set; }  

        public List<PostComment> PostComments { get; set; } 
    }
}
