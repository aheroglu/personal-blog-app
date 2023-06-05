using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPostCommentService : IGenericService<PostComment>
    {
        List<PostComment> GetListWithPost();
        List<PostComment> GetCommentWithPost(int id);
        List<PostComment> GetCommentsByPost(int id);
    }
}
