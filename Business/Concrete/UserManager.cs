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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void TDelete(User t)
        {
            _userDal.Delete(t);
        }

        public User TGetById(int id)
        {
            return _userDal.GetById(id);
        }

        public void TInsert(User t)
        {
            _userDal.Insert(t);
        }

        public List<User> TList()
        {
            return _userDal.List();
        }

        public void TUpdate(User t)
        {
            _userDal.Update(t);
        }
    }
}
