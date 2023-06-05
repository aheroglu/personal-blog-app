using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataAccess.EntityFramework
{
    public class EfTrainingCommentDal : GenericRepository<TrainingComment>, ITrainingCommentDal
    {
        public List<TrainingComment> GetCommentsByPost(int id)
        {
            using (var c = new Context())
            {
                return c.Set<TrainingComment>().Where(x => x.TrainingId == id && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<TrainingComment> GetCommentWithTraining(int id)
        {
            using (var c = new Context())
            {
                return c.Set<TrainingComment>().Include(x => x.Training).Where(x => x.Id == id).ToList();
            }
        }

        public List<TrainingComment> GetListWithTraining()
        {
            using (var c = new Context())
            {
                return c.Set<TrainingComment>().Include(x => x.Training).Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
