using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    internal interface ITrainingCommentService : IGenericService<TrainingComment>
    {
        public List<TrainingComment> GetListWithTraining();
        public List<TrainingComment> GetCommentsByPost(int id);
        public List<TrainingComment> GetCommentWithTraining(int id);
    }
}
