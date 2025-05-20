using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

//programın hepsini yetkiye alırsam, logout olduktan sonra her turlu istek icin login sayfasina mi giderim?
//authentication mode=Forms ise evet
namespace WebApplicationYeni.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        IUserService UserService = new UserBL(new EfUserDal());
        //mail, sms kontrolü eklenecek
        //
        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            Debug.WriteLine(FormsAuthentication.FormsCookieName);
            Debug.WriteLine(Request.Cookies.Count);

            return View();
        }


        //Role bilgisini Session a kaydetmek yerine AuthCookie ye kaydetme
        [HttpPost]
        public ActionResult Login(User user)
        {
            Debug.WriteLine(user.UserName);
            Debug.WriteLine(user.UserPassword);

            //Kendim FormsAuthentication Ticket ve Cookie yaratıp, Response a ekleyecem
            //Burada ExpireDate gibi seyleri ayarlayabilirim.
            //Ornek Cookie
            HttpCookie cookie = new HttpCookie("Selam");
            cookie.Value = "Server dan selamlar Cookie si";
            DateTime dt = DateTime.Now.AddDays(1);
            cookie.Expires = dt;
            Response.Cookies.Add(cookie);

            //user kontrolü
            User a = UserService.GetUserBl(user.UserName);
            if (a == null)
            {
                Debug.WriteLine("Verilen kullanici adi kayitli degil");
                return View();
            }
            else
            {
                Debug.WriteLine("user bulundu, userName:" + a.UserName);
                string encrypPass = a.UserPassword;
                Debug.WriteLine("encrypted password" + encrypPass);

                //eger kaydolan kullanici "234" gibi duzgun base64 encryptlenmis bir sifre icermiyorsa
                //format exception alırsın
                try
                {

                    string pass = Helper.CustomCrypto.Decrypt(encrypPass);
                    Debug.WriteLine("password:" + pass);

                    if (pass != user.UserPassword)
                    {
                        Debug.WriteLine("Verilen sifre, kullanici adi ile eslesmiyor");
                        ViewBag.Message = "Şifre veya kullanıcı adı yanlış";
                        return View();
                    }
                    //cookie degerini encrypt islemi, bunun icin FormsAuthentication sınıfı kullanılabilir mi?
                    //Kullanılabilir ama bu sınıf sanırım sadece bir formsAuthenticationTicket ı encrypt edebilir,
                    //onla calısmak icin tasarlanmıs?
                    //O zaman FormsAuthenticationTicket olusturuyoruz, Bu da name ve userData alanları iceriyor deger olarak
                    //name = userName, userData = userRole olacak
                    FormsAuthenticationTicket formsAuthTicket = new FormsAuthenticationTicket(0, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, a.UserRole);
                    string encrpytedTicket = FormsAuthentication.Encrypt(formsAuthTicket);
                    cookie = new HttpCookie(".ASPXAUTH");
                    cookie.Value = encrpytedTicket;
                    Response.Cookies.Add(cookie);
                    Session["UserId"] = a.UserId;

                    //formsAuthTicket = FormsAuthentication.Decrypt(Response.Cookies.Get(".ASPXAUTH").Value);

                    return RedirectToAction("GetAllKitaps", "Kitap");
                    //return formsAuthTicket.Expired.ToString() + "," + formsAuthTicket.IssueDate.ToString();
                }
                catch(FormatException fe)
                {
                    Debug.WriteLine(fe.Message);
                    ViewData["message"]= "Db ye User, password encrypt edilmeden eklenmiş. Silinp tekrar yüklenmesi lazım.";
                    return View();
                }
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        //kullanıcı kaydı islemi
        //---------------------------------------------------------------
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //Eklenen herkes otomatik olarak Role="B" atanıyor
        //Admin manuel olarak db den eklenir.
        [HttpPost]
        public ActionResult Register(User user)
        {
            Debug.WriteLine("user.UserName:" + user.UserName);
            Debug.WriteLine("user.UserPassword:" + user.UserPassword);
            user.UserRole = "B";

            user.UserPassword = Helper.CustomCrypto.Encrypt(user.UserPassword);
            Debug.WriteLine("user.UserPassword after encryption:" + user.UserPassword);
            UserService.AddUser(user);
            return RedirectToAction("Login");
        }

        //---------------------------------------------------------------
    }
}