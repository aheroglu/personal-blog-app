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
    public class TrainingManager : ITrainingService
    {
        ITrainingDal _trainingDal;

        public TrainingManager(ITrainingDal trainingDal)
        {
            _trainingDal = trainingDal;
        }

        public List<Training> GetTrainingWithCategory(int id)
        {
            return _trainingDal.GetTrainingWithCategory(id);
        }

        public List<Training> LatestFiveTraining()
        {
            return _trainingDal.LatestFiveTraining();
        }

        public List<Training> ListWithCategory()
        {
            return _trainingDal.ListWithCategory();
        }

        public List<Training> PopularPostsByCategory(int id)
        {
            return _trainingDal.PopularPostsByCategory(id);
        }

        public List<Training> PopularTrainingsList()
        {
            return _trainingDal.PopularTrainingsList();
        }

        public void TDelete(Training t)
        {
            _trainingDal.Delete(t);
        }

        public Training TGetById(int id)
        {
            return _trainingDal.GetById(id);
        }

        public void TInsert(Training t)
        {
            _trainingDal.Insert(t);
        }

        public List<Training> TList()
        {
            return _trainingDal.List();
        }

        public List<Training> TrainingByCategory(int id)
        {
            return _trainingDal.TrainingByCategory(id);
        }

        public void TUpdate(Training t)
        {
            _trainingDal.Update(t);
        }
    }
}
