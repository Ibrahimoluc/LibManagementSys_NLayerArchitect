using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AlanBL : IAlanService
    {
        //public GenericRepository<Alan> repo = new GenericRepository<Alan>();
        IAlanDal alanDal;

        public AlanBL(IAlanDal alanDal)
        {
            this.alanDal = alanDal;
        }
        

        public List<Alan> GetAllAlanBl()
        {
            return alanDal.List(); 
        }

        public void AddAlanBl(Alan a)
        {
            alanDal.Insert(a);
        }
        public Alan GetAlanByIdBl(int id)
        {
            return alanDal.Get(x => x.AlanId == id);
        }

        public void DeleteAlanBl(Alan a)
        {
            alanDal.Delete(a);
        }

        public void UpdateAlanBl(Alan a) 
        {
            alanDal.Update(a);     
        }
 
        //entity state denemesi icin
        public String GetAlanState(Alan a)
        {
            return alanDal.GetEntityStateString(a);
        }
        

        //istatisticController icin
        public List<Alan> GetAlanS(Expression<Func<Alan, bool>> filter)
        {
            return alanDal.List(filter);
        }


    }
}
