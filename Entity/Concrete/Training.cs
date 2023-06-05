using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entity.Concrete
{
    public class Training
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int ClickCount { get; set; }
        public bool Status { get; set; }

        public int TrainingCategoryId { get; set; }
        public TrainingCategory TrainingCategory { get; set; }

        public List<TrainingComment> TrainingComments { get; set; }
    }
}
