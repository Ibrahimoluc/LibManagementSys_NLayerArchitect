using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> filter);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        //yeni ekledim
        T GetById(int id);

        T Get(Expression<Func<T, bool>> filter);

        //entityState icin ekledim, biraz deneme amacli
        String GetEntityStateString(T entity);
    }
}
