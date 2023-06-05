using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITrainingService : IGenericService<Training>
    {
        public List<Training> ListWithCategory();
        public List<Training> PopularTrainingsList();
        public List<Training> GetTrainingWithCategory(int id);
        public List<Training> LatestFiveTraining();
        public List<Training> PopularPostsByCategory(int id);
        public List<Training> TrainingByCategory(int id);
    }
}
