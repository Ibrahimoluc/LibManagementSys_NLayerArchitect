using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace WebApplicationYeni.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        ICommentService commentService = new CommentBL(new EfCommentDal());
        IKitapService kitapService = new KitapBL(new EfKitapDal());

        [HttpGet]
        public ActionResult addComment(int id)
        {
            Kitap k = kitapService.GetKitapByIdBl(id);
            return View(k);
        }

        [HttpPost]
        public ActionResult addComment(Comment p)
        {
            commentService.addCommentBl(p);
            string str = String.Format("getCommentsById/{0}", p.KitapId);
            return RedirectToAction(str);
        }

        public ActionResult getCommentsById(int id)
        {
            ViewBag.Id = id;
            List<Comment> comms = commentService.GetCommentsByIdBl(id).ToList();
            //comms ların içinde zaten User ve Kitap da var.
            //O yuzden daha dısarıdan ekstra bilgi gondermeye ya da burada tekrar cekmeye gerek yok.
            return View(comms);
        }

        public ActionResult updateComment(int id)
        {
            return View(commentService.GetCommentByIdBl(id));
        }
    }
}