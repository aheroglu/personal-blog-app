using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITrainingDal : IGenericDal<Training>
    {
        public List<Training> ListWithCategory();
        public List<Training> PopularTrainingsList();
        public List<Training> GetTrainingWithCategory(int id);
        public List<Training> LatestFiveTraining();
        public List<Training> PopularPostsByCategory(int id);
        public List<Training> TrainingByCategory(int id);
    }
}
