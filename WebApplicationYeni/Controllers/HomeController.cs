using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer;
using EntityLayer.Concrete;
using Newtonsoft.Json;

namespace WebApplicationYeni.Controllers
{
    //Burasi her turlu konuda ve entityde bir seyleri denedigim extra bir yer   
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        public GenericRepository<Alan> repo = new GenericRepository<Alan>();
        //public GenericRepository<EntityLayer.Class1> repo2 = new GenericRepository<EntityLayer.Class1>();
        //public HomeController()
        //{
        //    repo = new GenericRepository<Alan>(); // DI ile enjekte etmek daha doğru olur
        //}

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users="kali")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        int t = 0;
        public ActionResult Contact()
        {
            Debug.WriteLine("Cookie sayisi:" + Request.Cookies.Count);  
            Debug.WriteLine("Cookie[0]:" + Request.Cookies.GetKey(0));
            Debug.WriteLine("Cookie[1]:" + Request.Cookies.GetKey(1));

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AlanEkleHome()
        {
            Alan alan = new Alan();
            alan.AlanName = "Tarih";
            repo.Insert(alan);
            return View();
        }

        public ActionResult AlanList()
        {
            List<Alan> alans = repo.List();
            String str = "";
            foreach (Alan a in alans)
            {
                str = str + a.AlanName + '\n';
            }
            ViewBag.Message = str;
            return View();
        }

        public ActionResult TemaDeneme()
        {
            return View();
        }

        //--------EntityStateDenemeleri-------
        //
        //Detached(Bagimsiz)
        //Sanirim DbSet in icinde olmayan, yani database den cekmedigimiz entityler.
        public ActionResult DenemeDetached() //Detached donduruyor
        {
            //DbContext de bulunmayan bir class icin Entry denemesi, bakalim onun icin entry cagiriliyor mu? Yok
            //EntityLayer.Class1 class1 = new EntityLayer.Class1();
            //String class1_entry_state = repo2.GetEntityStateString(class1);

            Alan alan = new Alan();
            String str_state = repo.GetEntityState(alan).ToString();
            ViewData["EnState"] = str_state;
            return View("DenemeEntityState");
        }

        public ActionResult DenemeUnchanged() //Unchaged donduruyor
        {
            //Alan alan = repo.GetById(102);
            //Direkt database den almak yerine, sadece id ye baktigini dusunup
            //yani id ye bakip, bu benim contextimde var diyor olabilir mi?
            //onun denemesi, ise yaramiyor
            Alan alan = new Alan();
            //yasak, diyor ki context bu nesneyi takip ediyor ve bu nesnenin key ini degistiremezsin
            //eger new Alan ise degistirilir
            alan.AlanId = 102;

            alan.AlanName = "geometri";
            String str_state = repo.GetEntityState(alan).ToString();
            ViewData["EnState"] = str_state;
            return View("DenemeEntityState");
        }

        public ActionResult DenemeChanged() //Modified donduruyor
        {
            Alan alan = repo.GetById(14);
            alan.AlanName = "degistiririm";
            String str_state = repo.GetEntityState(alan).ToString();
            ViewData["EnState"] = str_state;
            return View("DenemeEntityState");
        }

        //GenericRepo nasıl davranıyor? Cagirilinca direkt istenen tablonun butun kayıtlarını baslangıcta cekiyor mu?
        //Context dedigimiz sey bu mu oluyor, DbSet objects icindeki veriler?
        //Eger islem bitmeden database e yeni bir kayıt eklenirse ilgili tabloya,
        //burası onu goremeyecek mi yeni bir istege kadar?
        public void DbSetAlanDeneme()
        {
            Debug.WriteLine("ilk çekim:" + repo.objects.Count());
            Debug.WriteLine("Guncellemeden sonra çekim:" + repo.objects.Count());
        }


        //---------------------------API denemeleri-------------------------------
        //API denemeleri, istekleri de "ConsoleAppAsWebClient" programından atıcam
        public string Selamlama()
        {
            return "Hosgeldin kullanici";
        }

        [Authorize]
        [HttpGet]
        public string OzelSelamlama(String name)
        {
            return "Hosgeldin" + name;
        }

        [HttpPost]
        public string OzelSelamlamaPost(String name, int? id)
        {
            return "Hosgeldin " + name + ", id:" + id;
        }

        //Selamlama json versiyonu yazilacak
        //Kendisine gelen Http isteğinin header ındaki Content-Type a bakacak
        //application/json oldugunu gorunce ona gore yorumlayacak
        [HttpPost]  
        public string OzelSelamlamaJson(String name, int? id)
        {
            return "Json, Hosgeldin " + name + ", id:" + id;
        }

        //Alan icin de yazilacak
        public string getAlanById()
        {
            //102 tarih
            Alan a = repo.GetById(102);
            return JsonConvert.SerializeObject(a);
        }

    }
}