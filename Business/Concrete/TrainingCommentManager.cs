using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TrainingCommentManager : ITrainingCommentService
    {
        ITrainingCommentDal _trainingCommentDal;

        public TrainingCommentManager(ITrainingCommentDal trainingCommentDal)
        {
            _trainingCommentDal = trainingCommentDal;
        }

        public List<TrainingComment> GetCommentsByPost(int id)
        {
            return _trainingCommentDal.GetCommentsByPost(id);
        }

        public List<TrainingComment> GetCommentWithTraining(int id)
        {
            return _trainingCommentDal.GetCommentWithTraining(id);
        }

        public List<TrainingComment> GetListWithTraining()
        {
            return _trainingCommentDal.GetListWithTraining();
        }

        public void TDelete(TrainingComment t)
        {
            _trainingCommentDal.Delete(t);
        }

        public TrainingComment TGetById(int id)
        {
            return _trainingCommentDal.GetById(id);
        }

        public void TInsert(TrainingComment t)
        {
            _trainingCommentDal.Insert(t);
        }

        public List<TrainingComment> TList()
        {
            return _trainingCommentDal.List();
        }

        public void TUpdate(TrainingComment t)
        {
            _trainingCommentDal.Update(t);
        }
    }
}
