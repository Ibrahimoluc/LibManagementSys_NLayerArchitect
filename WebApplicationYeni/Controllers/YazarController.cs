using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace WebApplicationYeni.Controllers
{
    public class YazarController : Controller
    {
        IYazarService yazarService;
        public YazarController()
        {
            yazarService = new YazarBL(new EfYazarDal());
        }

        public ActionResult GetAllYazar() 
        { 
            return View(yazarService.GetAllYazarBl());
        }

        [HttpGet]
        [Authorize(Roles = "A")]
        public ActionResult YazarEkle()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public ActionResult YazarEkle(Yazar y)
        {
            Debug.WriteLine("yazarId:" + y.YazarId);
            Debug.WriteLine("yazarName:" + y.YazarName);
            Debug.WriteLine("yazarDTarih:" + y.YazarDTarih);

            yazarService.AddYazarBl(y);
            return RedirectToAction("GetAllYazar");
        }

        [Authorize(Roles = "A")]
        public ActionResult DeleteYazar(int id)
        {
            yazarService.DeleteYazarBl(yazarService.GetYazarByIdBl(id));
            return RedirectToAction("GetAllYazar");
        }


        [Authorize(Roles = "A")]
        public ActionResult UpdateYazar(int id)
        {
            //ViewData["id"] = id;
            return View(yazarService.GetYazarByIdBl(id));
        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public ActionResult UpdateYazar(Yazar y)
        {
            Debug.WriteLine("yazarID" + y.YazarId);
            yazarService.UpdateYazarBl(y);
            return RedirectToAction("GetAllYazar");
        }


        //Acaba getById gibi bir methodla silinmek istenen nesne çekilmeden de silme islemi gerceklestirilebiliyor mu?
        //Yani istenen id nin oldugu bir nesne verilsin genericRepo ya yeter mi? Evet
        //GenericRepo sadece id ye gore db ye bir sorgu atıyor olabilir?
        public void DeleteYazar2(int id)
        {
            Yazar yazar = new Yazar();
            yazar.YazarId = id;
            yazarService.DeleteYazarBl(yazar);
            
        }
    }
}