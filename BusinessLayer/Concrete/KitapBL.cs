using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class KitapBL : IKitapService
    {
        //IKitapDal KitapDal;
        //Eger IKitapDal sadece IRepo inherit aliyor ve extra bir sey koymuyorsa burada o da cagirilip
        //IKitapDal implemente eden EfAlanDal class ının bir objesi verilirse yine calisir mi? Evet
        IRepository<Kitap> KitapDal;

        public KitapBL(IKitapDal kitapDal)
        {
            KitapDal = kitapDal;
        }

        public List<Kitap> GetAllKitapsBl()
        {
            return KitapDal.List();
        }

        public void KitapEkleBl(Kitap k)
        {
            KitapDal.Insert(k);
        }

        public List<Kitap> GetKitapsByIdBl(int yazarId)
        {
            return KitapDal.List(x => x.YazarId == yazarId);
        }

        public Kitap GetKitapByIdBl(int id)
        {
            return KitapDal.GetById(id);
        }

        public void UpdateKitapBl(Kitap k)
        {
            KitapDal.Update(k);
        }

        public void DeleteKitapBl(Kitap k)
        {
            KitapDal.Update(k);
        }

        public List<Kitap> GetKitapsByAlanId(int id)
        {
            return KitapDal.List(x => x.AlanId == id);
        }


    }
}
