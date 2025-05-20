using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IKitapService
    {
        List<Kitap> GetAllKitapsBl();

        //yazara gore Kitaplari getirme
        List<Kitap> GetKitapsByIdBl(int yazarId);

        //Alan gore getirme icin de ayri fonksiyon yazilabilir ya da 
        //.List(Expression)'ı burada kullanan bir fonksiyon ile hepsi tek bir yerde birlesir ama GPT yapma diyor.
        List<Kitap> GetKitapsByAlanId(int id);

        void KitapEkleBl(Kitap kitap);

        Kitap GetKitapByIdBl(int id);

        void UpdateKitapBl(Kitap k);

        //Status degistirir
        void DeleteKitapBl(Kitap k);

    }
}
