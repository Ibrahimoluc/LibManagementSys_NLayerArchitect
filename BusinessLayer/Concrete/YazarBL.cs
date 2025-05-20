using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;


namespace BusinessLayer.Concrete
{
    public class YazarBL : IYazarService
    {
        IYazarDal yazarDal;

        public YazarBL(IYazarDal yazarDal)
        {
            this.yazarDal = yazarDal;
        }

        public List<Yazar> GetAllYazarBl()
        {
            return yazarDal.List();
        }

        public void AddYazarBl(Yazar y)
        {
            yazarDal.Insert(y);
        }

        public Yazar GetYazarByIdBl(int id)
        {
            return yazarDal.Get(x => x.YazarId == id);
        }

        //kitapEkle icinde kullaniyorum
        public Yazar GetYazarBl(Yazar y)
        {
            return yazarDal.Get(x => x.YazarName == y.YazarName && x.YazarDTarih == y.YazarDTarih );
        }

        public void DeleteYazarBl(Yazar y)
        {
            yazarDal.Delete(y);
        }

        public void UpdateYazarBl(Yazar y)
        {
            yazarDal.Update(y);
        }

        public string GetYazarState(Yazar y)
        {
            return yazarDal.GetEntityStateString(y);
        }
    }
}
