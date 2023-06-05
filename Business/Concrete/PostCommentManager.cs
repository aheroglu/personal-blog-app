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
    public class PostCommentManager : IPostCommentService
    {
        IPostCommentDal _postCommentDal;

        public PostCommentManager(IPostCommentDal postCommentDal)
        {
            _postCommentDal = postCommentDal;
        }

        public List<PostComment> GetCommentsByPost(int id)
        {
            return _postCommentDal.GetCommentsByPost(id);
        }

        public List<PostComment> GetCommentWithPost(int id)
        {
            return _postCommentDal.GetCommentWithPost(id);
        }

        public List<PostComment> GetListWithPost()
        {
            return _postCommentDal.GetListWithPost();
        }

        public void TDelete(PostComment t)
        {
            _postCommentDal.Delete(t);
        }

        public PostComment TGetById(int id)
        {
            return _postCommentDal.GetById(id);
        }

        public void TInsert(PostComment t)
        {
            _postCommentDal.Insert(t);
        }

        public List<PostComment> TList()
        {
            return _postCommentDal.List();
        }

        public void TUpdate(PostComment t)
        {
            _postCommentDal.Update(t);
        }
    }
}
