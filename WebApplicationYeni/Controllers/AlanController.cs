using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace WebApplicationYeni.Controllers
{
    public class AlanController : Controller
    {
        public AlanBL aBl = new AlanBL(new EfAlanDal());
        public KitapBL kitapService = new KitapBL(new EfKitapDal());

        public ActionResult GetAllAlan()
        {
            Session["AlandanGelen"] = "alandan gelen session";
            var aList = aBl.GetAllAlanBl();
            return View(aList);
        }

        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult AlanEkle()
        {
            Debug.WriteLine("AlanEkleGet cagiriliyor");
            return View();
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AlanEkle(Alan a)
        {
            Debug.WriteLine("AlanEkle Post cagiriliyor");
            AlanValidator alanValidator = new AlanValidator();
            ValidationResult validationResult = alanValidator.Validate(a);
            //debug icin
            Debug.WriteLine(validationResult.Errors.Count);
            if (validationResult.IsValid)
            {
                aBl.AddAlanBl(a);
                return RedirectToAction("GetAllAlan");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    //fluentdeki error u System.Model deki errora ekliyoruz
                    //Sanirim bu ValidationResult kisminda System icindeki degil fluenti kullandigimiz icin
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }


        //----------------GetAlanById
        public ActionResult GetAlanById(int? id)
        {

            Debug.WriteLine("gelen id:" + id);
            Alan alan = null;
            if (id == null) return View(alan);

            int a_id = (int)id;
            alan = aBl.GetAlanByIdBl(a_id);

            if (alan == null)
            {
                Debug.WriteLine("alan null");
                return RedirectToAction("Page404", "Error");
            }
            else
            {
                Debug.WriteLine("gelen id:" + alan.AlanId + "; alanName:" + alan.AlanName);
                alan.Kitaps = kitapService.GetKitapsByAlanId(alan.AlanId);
                return View(alan);

            }

        }


        //-------------------------

        //------------Delete-----------

        //listenin üzerinden silme
        //burada yukaridaki nullable, null kontrol eklemedim, cunku liste uzerinden,
        //yani zaten olan seyler uzerinden silme seciliyor
        [Authorize(Roles = "A")]
        public ActionResult DeleteAlan(int id)
        {
            Alan alan = aBl.GetAlanByIdBl(id);
            aBl.DeleteAlanBl(alan);
            return RedirectToAction("GetAllAlan");
        }

        //--------------Update-----------------
        //GetAllAlan dan Update e yonlendirme icin
        [HttpGet]
        [Authorize(Roles = "A")]
        public ActionResult UpdateAlan(int id)
        {
            ViewData["id"] = id;

            return View();
        }

        //Guncellemek icin 
        [HttpPost]
        [Authorize(Roles = "A")]
        public ActionResult UpdateAlan(Alan a)
        {
            AlanValidator alanValidator = new AlanValidator();
            ValidationResult validationResult = alanValidator.Validate(a);
            //debug icin
            Debug.WriteLine(validationResult.Errors.Count);
            if (validationResult.IsValid)
            {
                aBl.UpdateAlanBl(a);
                return RedirectToAction("GetAllAlan");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    //fluentdeki error u System.Model deki errora ekliyoruz
                    //Sanirim bu ValidationResult kisminda System icindeki degil fluenti kullandigimiz icin
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }


            ViewData["id"] = a.AlanId;
            return View();
        }

        public ActionResult AlanIstatistic()
        {
            List<Alan> list = aBl.GetAllAlanBl();
            int toplam = list.Count();
            ViewData["toplam"] = toplam;

            int grupSayisi = list.GroupBy(x => x.AlanName).Count();
            

            var linqExpress = from alan in list
                              group alan by alan.AlanName;

            ViewBag.AlanNames = linqExpress.Select(x => x.Key).ToArray();
            ViewBag.Counts = linqExpress.Select(x => x.ToList().Count()).ToArray();

            IGrouping<string, Alan> MaxGroup = linqExpress.Aggregate((a,b) => (a.ToList().Count > b.ToList().Count) ? a : b);
            int max = MaxGroup.ToList().Count;
            String maxName = MaxGroup.Key;
            
            Debug.WriteLine(linqExpress.Count());
            Debug.WriteLine(max);

            ViewData["max"] = max;
            ViewData["maxName"] = maxName;
            ViewData["buyuk10"] = linqExpress.Where(x => x.ToList().Count > 10).Count();

            return View(list);
        }



        //--------denemeler------------
        //stringQuery den alan bilgilerini alip ekleme, Alan icin tek bilgi su an Alan.AlanName
        //Su sekil bir Url ile calisir: http://localhost:51215/Alan/DenemeAlanEkle?AlanId=7&AlanName=Test
        [HttpGet]
        public ActionResult DenemeAlanEkle(Alan a)
        {
            Debug.WriteLine("AlanId:" + a.AlanId + ";AlanName:" + a.AlanName);

            aBl.AddAlanBl(a);
            return View();
        }

        //------EntityState Denemeleri Home da. Cunku buradan direkt GenericRepo ya erisilemiyor
        //sadece aBl nin repo yu kullanarak sundugu siniflara erisiliyor.

    }
}