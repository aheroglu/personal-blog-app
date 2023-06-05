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
    public class PostCategoryManager : IPostCategoryService
    {
        IPostCategoryDal _postCategoryDal;

        public PostCategoryManager(IPostCategoryDal postCategoryDal)
        {
            _postCategoryDal = postCategoryDal;
        }

        public void TDelete(PostCategory t)
        {
            _postCategoryDal.Delete(t);
        }

        public PostCategory TGetById(int id)
        {
            return _postCategoryDal.GetById(id);
        }

        public void TInsert(PostCategory t)
        {
            _postCategoryDal.Insert(t);
        }

        public List<PostCategory> TList()
        {
            return _postCategoryDal.List();
        }

        public void TUpdate(PostCategory t)
        {
            _postCategoryDal.Update(t);
        }
    }
}
