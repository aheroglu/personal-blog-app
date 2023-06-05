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
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public List<Message> MessageById(int id)
        {
            return _messageDal.MessageById(id);
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetById(int id)
        {
            return _messageDal.GetById(id);
        }

        public void TInsert(Message t)
        {
            _messageDal.Insert(t);
        }

        public List<Message> TList()
        {
            return _messageDal.List();
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }
    }
}
