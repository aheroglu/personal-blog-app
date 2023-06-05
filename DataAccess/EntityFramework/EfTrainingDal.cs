using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.EntityFramework
{
    public class EfTrainingDal : GenericRepository<Training>, ITrainingDal
    {
        public List<Training> GetTrainingWithCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.Id == id).ToList();
            }
        }

        public List<Training> LatestFiveTraining()
        {
            using (var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.Status == true && x.TrainingCategory.Status == true).OrderByDescending(x => x.Id).Take(5).ToList();
            }
        }

        public List<Training> ListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.Status == true && x.TrainingCategory.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Training> PopularPostsByCategory(int id)
        {
            using(var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.TrainingCategoryId == id && x.Status == true).OrderByDescending(x => x.ClickCount).Take(6).ToList();
            }
        }

        public List<Training> PopularTrainingsList()
        {
            using (var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.Status == true && x.TrainingCategory.Status == true).OrderByDescending(x => x.ClickCount).Take(6).ToList();
            }
        }

        public List<Training> TrainingByCategory(int id)
        {
            using(var c = new Context())
            {
                return c.Set<Training>().Include(x => x.TrainingCategory).Where(x => x.TrainingCategoryId == id && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
