using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
    {
        void addCommentBl(Comment p);

        List<Comment> GetAllCommentsBl();

        List<Comment> GetCommentsByIdBl(int id);

        Comment GetCommentByIdBl(int id);
    }
}
