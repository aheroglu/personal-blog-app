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
    public class EfPostDal : GenericRepository<Post>, IPostDal
    {
        public List<Post> GetPostWithCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.Id == id).ToList();
            }
        }

        public List<Post> LatestFivePost()
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.Status == true && x.PostCategory.Status == true).OrderByDescending(x => x.Id).Take(5).ToList();
            }
        }

        public List<Post> ListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.Status == true && x.PostCategory.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Post> PopularPostsByCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.PostCategoryId == id && x.Status == true).OrderByDescending(x => x.ClickCount).Take(6).ToList();
            }
        }

        public List<Post> PopularPostsList()
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.Status == true && x.PostCategory.Status == true).OrderByDescending(x => x.ClickCount).Take(6).ToList();
            }
        }

        public List<Post> PostsByCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Set<Post>().Include(x => x.PostCategory).Where(x => x.PostCategoryId == id && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

    }
}
