using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
 
namespace WebApplicationYeni.Controllers
{
    public class KitapController : Controller
    {
        IKitapService kitapBl;
        //kaldırılabilir
        IYazarService yazarBl;
        IAlanService alanBl;


        public KitapController()
        {
            kitapBl = new KitapBL(new EfKitapDal());
            yazarBl = new YazarBL(new EfYazarDal());
            alanBl = new AlanBL(new EfAlanDal());
        }

        [Authorize]
        public ActionResult GetAllKitaps()
        {
            List<Kitap> kitaps = kitapBl.GetAllKitapsBl();
            return View(kitaps);
        }

        
        [Authorize(Roles = "A")]
        public ActionResult KitapEkle(Alan a)
        {
            //Alanlar icin drop down liste gonderilecek list
            List<SelectListItem> alans = (from x in alanBl.GetAllAlanBl()
                                         select new SelectListItem
                                         {
                                             Text = x.AlanName,
                                             Value = x.AlanId.ToString()
                                         }).ToList();
            ViewBag.Dlist = alans;

            return View(a);
        }



        //KitapEkle async deneme
        //kitapDosya.SaveAs(...) diske dosyayı yazma işlemi
        //Bunun async olmasi lazim sanirim?
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult KitapEkle(Kitap k, HttpPostedFileBase kitapDosya)
        {

            Yazar y = yazarBl.GetYazarBl(k.Yazar);
            if (null == y)
            {
                Debug.WriteLine("Girdiginiz yazar daha onceden eklenmemis");
                ViewBag.Message = "Yeni yazar olusturuldu";
            }
            else
            {
                k.YazarId = y.YazarId; //k.Yazar = y; 
                k.Yazar = null;

            }

            k.KitapStatus = true;

            HttpPostedFileWrapper httpPostedFileWrapper = null;
            //sonradan ekledigim kitap dosyasi islemleri
            //--------------------------
            //kitapDosya icin bir sey gondermezsen sıkıntı yok, demekki null olabilen bir tür
            if (kitapDosya != null)
            {
                //Normalde Abstract Class olan kitapDosya icin MVC icindeki ModelBinder hangi class ı atıyor
                Debug.WriteLine("Tip: " + kitapDosya.GetType().FullName);

                Debug.WriteLine("kitapDosya türü:" + kitapDosya.GetType());
                Debug.WriteLine("KitapDosya var, kitapDosya.FileName:" + kitapDosya.FileName);
                Debug.WriteLine("Uzunluk:" + kitapDosya.ContentLength);
                string dosyaYolu = Path.GetFileName(kitapDosya.FileName);
                string kitapYolu = String.Format("/Dosyalar/{0}", dosyaYolu);
                Debug.Write(dosyaYolu);
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Dosyalar"), dosyaYolu);
                Debug.WriteLine("dosyaYolu:" + dosyaYolu);
                Debug.WriteLine("kitapYolu:" + kitapYolu);
                Debug.WriteLine("yuklemeYeri:" + yuklemeYeri);
                try
                {
                    kitapDosya.SaveAs(yuklemeYeri);
                    k.KitapYolu = kitapYolu;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception atıldı hooppp");
                    return RedirectToAction("GetAllKitaps");
                }

            }

            //--------------------------

            kitapBl.KitapEkleBl(k);


            return RedirectToAction("GetAllKitaps");
        }





        public ActionResult getKitapDosyaById(int id)
        {
            Debug.WriteLine("id:" + id);
            Kitap k = kitapBl.GetKitapByIdBl(id);
            Debug.WriteLine("Kitap id:" + k.KitapId);
            Debug.WriteLine("Kitap Yolu:" + k.KitapYolu);
            ViewBag.kitapYolu = k.KitapYolu;
            return View();
        }


        //belli bir yazarin id sini alip onun kitaplarini getirme
        public ActionResult GetKitapsByYazarId(int yazarId)
        {
            return View("GetAllKitaps", kitapBl.GetKitapsByIdBl(yazarId));
        }


        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult UpdateKitap(int id)
        {   
            return View(kitapBl.GetKitapByIdBl(id));
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult UpdateKitap(Kitap k, HttpPostedFileBase kitapDosya)
        {
            //k.Yazar null ise duzgun calisiyor, degilse Id lerinin esit olmasini istiyor,(k.YazarId ve k.Yazar.YazarId)
            if (k.Yazar == null) Debug.WriteLine("k.Yazar null");
            else
            {
                Debug.WriteLine("k.YazarId" + k.YazarId);
                Debug.WriteLine("k.Yazar.YazarId" + k.Yazar.YazarId);
                Debug.WriteLine("k.Yazar.YazarName" + k.Yazar.YazarName);
                Debug.WriteLine("k.Yazar.YazarDTarih" + k.Yazar.YazarDTarih);
            }

            Yazar y = yazarBl.GetYazarBl(k.Yazar);
            if(null == y)
            {
                Debug.WriteLine("Girdiginiz kriterlerde bir yazar bulunamadi, yazar ekleniyor");
                yazarBl.AddYazarBl(k.Yazar);
                y = yazarBl.GetYazarBl(k.Yazar);
                if(null == y) Debug.WriteLine("Eklenen yazar cekilemedi");
                k.YazarId = y.YazarId;
            }
            Debug.WriteLine("y.YazarId" + y.YazarId);
            Debug.WriteLine("y.YazarName" + y.YazarName);
            Debug.WriteLine("y.YazarDTarih" + y.YazarDTarih);
            k.Yazar = y;

            //file islemi
            if (kitapDosya != null)
            {
                Debug.WriteLine("kitapDosya türü:" + kitapDosya.GetType());
                Debug.WriteLine("KitapDosya var, kitapDosya.FileName:" + kitapDosya.FileName);
                Debug.WriteLine("Uzunluk:" + kitapDosya.ContentLength);
                string dosyaYolu = Path.GetFileName(kitapDosya.FileName);
                string kitapYolu = String.Format("/Dosyalar/{0}", dosyaYolu);
                Debug.Write(dosyaYolu);
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Dosyalar"), dosyaYolu);
                Debug.WriteLine("dosyaYolu:" + dosyaYolu);
                Debug.WriteLine("kitapYolu:" + kitapYolu);
                Debug.WriteLine("yuklemeYeri:" + yuklemeYeri);
                try
                {
                    kitapDosya.SaveAs(yuklemeYeri);
                    k.KitapYolu = kitapYolu;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception atıldı hooppp");
                    return RedirectToAction("GetAllKitaps");
                }

            }

            kitapBl.UpdateKitapBl(k);
            return RedirectToAction("GetAllKitaps");
        }

        [Authorize(Roles = "A")]
        public ActionResult DeleteKitap(int id)
        {
            Kitap k = kitapBl.GetKitapByIdBl(id);
            k.KitapStatus = false;
            kitapBl.DeleteKitapBl(k);
            return RedirectToAction("GetAllKitaps");
        }

        public PartialViewResult PartialDeneme()
        {
            return PartialView();
        }

        public ActionResult KitapIstatistic()
        {

            return View();
        }

        //KitapIstatisticler icin chart
        public PartialViewResult KitapChart() 
        {
            var linqExpression = from k in kitapBl.GetAllKitapsBl()
                                 group k by k.Alan.AlanName;

            ViewBag.xValues = linqExpression.Select(x => x.Key).ToArray();
            ViewBag.yValues = linqExpression.Select(x => x.ToList().Count).ToArray();

            return PartialView();
        }

        
      
    }
}