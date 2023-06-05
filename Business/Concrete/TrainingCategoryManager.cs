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
    public class TrainingCategoryManager : ITrainingCategoryService
    {
        ITrainingCategoryDal _trainingCategoryService;

        public TrainingCategoryManager(ITrainingCategoryDal trainingCategoryService)
        {
            _trainingCategoryService = trainingCategoryService;
        }

        public void TDelete(TrainingCategory t)
        {
            _trainingCategoryService.Delete(t);
        }

        public TrainingCategory TGetById(int id)
        {
            return _trainingCategoryService.GetById(id);    
        }

        public void TInsert(TrainingCategory t)
        {
            _trainingCategoryService.Insert(t);
        }

        public List<TrainingCategory> TList()
        {
            return _trainingCategoryService.List();
        }

        public void TUpdate(TrainingCategory t)
        {
            _trainingCategoryService.Update(t);
        }
    }
}
