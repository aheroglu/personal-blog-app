using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPostDal : IGenericDal<Post>
    {
        List<Post> ListWithCategory();
        List<Post> PopularPostsList();
        List<Post> GetPostWithCategory(int id);
        List<Post> LatestFivePost();
        List<Post> PopularPostsByCategory(int id);
        List<Post> PostsByCategory(int id);
    }
}
