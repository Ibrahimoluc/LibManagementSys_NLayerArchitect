using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IAlanService
    {
        List<Alan> GetAllAlanBl();

        void AddAlanBl(Alan a);

        Alan GetAlanByIdBl(int id);

        void DeleteAlanBl(Alan a);

        List<Alan> GetAlanS(Expression<Func<Alan, bool>> filter);

        void UpdateAlanBl(Alan a);
    }
}
