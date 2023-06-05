using System;

namespace Entity.Concrete
{
    public class PostComment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
