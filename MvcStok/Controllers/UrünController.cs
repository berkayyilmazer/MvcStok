
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrünController : Controller
    {
        MvcDbStokEntities2 db = new MvcDbStokEntities2();
        // GET: Urün

        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrünEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrünEkle(TBLURUNLER p1)
        {
            
            var ktg = db.TBLKATEGORILER.Where(x => x.KATEGORIID == p1.URUNKATEGORI).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Sil(int id)
        {
            var urün = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urün);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrünGetir(int id)
        {
            var urn = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrünGetir",urn);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urn = db.TBLURUNLER.Find(p1.URUNID);
            urn.URUNAD = p1.URUNAD;
            //urn.URUNKATEGORI = p1.URUNKATEGORI;
            var urun = db.TBLKATEGORILER.Where(x => x.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = urun.KATEGORIID;
            urn.MARKA = p1.MARKA;
            urn.FIYAT = p1.FIYAT;
            urn.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}