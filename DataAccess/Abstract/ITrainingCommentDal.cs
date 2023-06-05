using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITrainingCommentDal : IGenericDal<TrainingComment>
    {
        public List<TrainingComment> GetListWithTraining();
        public List<TrainingComment> GetCommentsByPost(int id);
        public List<TrainingComment> GetCommentWithTraining(int id);
    }
}
