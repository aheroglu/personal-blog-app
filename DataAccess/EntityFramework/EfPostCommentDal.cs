using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfPostCommentDal : GenericRepository<PostComment>, IPostCommentDal
    {
        public List<PostComment> GetCommentsByPost(int id)
        {
            using (var c = new Context())
            {
                return c.Set<PostComment>().Include(x => x.Post).Where(x => x.PostId == id && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<PostComment> GetCommentWithPost(int id)
        {
            using (var c = new Context())
            {
                return c.Set<PostComment>().Include(x => x.Post).Where(x => x.Id == id).ToList();
            }
        }

        public List<PostComment> GetListWithPost()
        {
            using (var c = new Context())
            {
                return c.Set<PostComment>().Include(x => x.Post).Where(x => x.Status == true && x.Post.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
