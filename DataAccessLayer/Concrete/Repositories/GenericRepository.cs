using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        //burasi onceden protected degildi
        protected Context c = new Context();
        //bu public degildi, debug icin yapildi, tekrar normal hale getirilebilir
        public DbSet<T> objects;

        public GenericRepository()
        {
            objects = c.Set<T>();
         //   Debug.WriteLine("ilk basta count:" + objects.Count());
            //Debugger ile burada durdurup, database de degisiklik yapildiktan sonra alttakini calistir
         //  Debug.WriteLine("database de degisiklik yapildiktan sonra count:" + objects.Count());
        }


        //Bu controller tarafında getAlanById siz de calisir ama verdigin Id tabloda yoksa sıkıntı
        public void Delete(T entity)
        {
            c.Entry(entity).State = EntityState.Deleted;
            c.SaveChanges();
        }

        public List<T> List()
        {
            return objects.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return objects.Where(filter).ToList();
        }

        //virtual sonradan eklendi, override edilmesi icin şartmış
        public virtual void Insert(T entity) 
        {
            //objects.Add(entity);
            c.Entry(entity).State = EntityState.Added;
            c.SaveChanges();
        }

        public void Update(T entity)
        {
            c.Entry(entity).State = EntityState.Modified;
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            return objects.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return objects.SingleOrDefault(filter);
        }

        //entityState icin eklenen method, entity nin state ini donduru
        public EntityState GetEntityState(T entity)
        {
            //var ent = c.Entry(entity);
            //ent.State = EntityState.Unchanged;
            //return c.Entry(entity).State;
            return c.Entry(entity).State;
        }

        public String GetEntityStateString(T entity)
        {
            return c.Entry(entity).State.ToString();
        }


        //butun contexti disari acmadan sadece saveChanges methodunu acma isi
        protected void SaveChanges()
        {
            c.SaveChanges();
        }
    }
}
