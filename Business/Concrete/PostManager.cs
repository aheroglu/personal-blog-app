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
    public class PostManager : IPostService
    {
        IPostDal _postDal;

        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public List<Post> GetPostWithCategory(int id)
        {
            return _postDal.GetPostWithCategory(id);
        }

        public List<Post> LatestFivePost()
        {
            return _postDal.LatestFivePost();
        }

        public List<Post> ListWithCategory()
        {
            return _postDal.ListWithCategory();
        }

        public List<Post> PopularPostsByCategory(int id)
        {
            return _postDal.PopularPostsByCategory(id);
        }

        public List<Post> PopularPostsList()
        {
            return _postDal.PopularPostsList();
        }

        public List<Post> PostsByCategory(int id)
        {
            return _postDal.PostsByCategory(id);
        }

        public void TDelete(Post t)
        {
            _postDal.Delete(t);
        }

        public Post TGetById(int id)
        {
            return _postDal.GetById(id);
        }

        public void TInsert(Post t)
        {
            _postDal.Insert(t);
        }

        public List<Post> TList()
        {
            return _postDal.List();
        }

        public void TUpdate(Post t)
        {
            _postDal.Update(t);
        }
    }
}
