using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CommentBL : ICommentService
    {
        ICommentDal comDal;

        public CommentBL(ICommentDal comDal)
        {
            this.comDal = comDal;
        }

        public void addCommentBl(Comment p)
        {
            comDal.Insert(p);
        }

        public List<Comment> GetAllCommentsBl()
        {
            return comDal.List();
        }

        public List<Comment> GetCommentsByIdBl(int id)
        {
            return comDal.List(x => x.KitapId == id);
        }

        public Comment GetCommentByIdBl(int id)
        {
            return comDal.GetById(id);
        }
    }
}
