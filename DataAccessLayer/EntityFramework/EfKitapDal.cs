using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfKitapDal : GenericRepository<Kitap>, IKitapDal
    {
        //public override void Insert(Kitap kitap)
        //{
        //    int flag = -1;
        //    if (c.Entry(kitap.Yazar).State == System.Data.Entity.EntityState.Unchanged)
        //    {
        //        Debug.WriteLine("flag 1 olcak");
        //        //yani kitabın yazarı database de var
        //        flag = 1;
        //    }
        //    else if (c.Entry(kitap.Yazar).State == System.Data.Entity.EntityState.Detached)
        //    {
        //        //kitabın yazarı database de yok
        //        Debug.WriteLine("flag 0 olcak");
        //        flag = 0;
        //    }
        //    else
        //    {
        //        throw new Exception("Kitap ne Detached ne Unchanged");
        //    }
            
        //    base.c.Entry(kitap).State = System.Data.Entity.EntityState.Added;

        //    if (flag == 1) base.c.Entry(kitap.Yazar).State = System.Data.Entity.EntityState.Unchanged;
        //    Debug.WriteLine(base.c.Entry(kitap.Yazar).State.ToString());

        //    base.SaveChanges();

        //}

    }
}
