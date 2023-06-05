using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public List<Message> MessageById(int id)
        {
            using (var c = new Context())
            {
                return c.Set<Message>().Where(x => x.Id == id).ToList();
            }
        }
    }
}
