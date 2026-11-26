using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlowDb.Models;

namespace GlowDb.Controllers
{
    public class UrunController : Controller
    {
        GlowDbEntities db = new GlowDbEntities();

      
        public ActionResult Index()
        {
            var urunler = db.Urunler.ToList();
            return View(urunler);
        }

        public ActionResult Ekle()
        {
            ViewBag.KategoriID = new SelectList(
                db.Kategoriler.ToList(), "KategoriID", "KategoriAdi");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Urunler urun)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urun);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(
                db.Kategoriler.ToList(),
                "KategoriID", "KategoriAdi", urun.KategoriID);

            return View(urun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var urun = db.Urunler.Find(id);

            if (urun != null)
            {
                db.Urunler.Remove(urun);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var urun = db.Urunler.Find(id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriID = new SelectList(
                db.Kategoriler.ToList(),
                "KategoriID", "KategoriAdi", urun.KategoriID);

            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Urunler urun)
        {
            if (ModelState.IsValid)
            {
                var eskiUrun = db.Urunler.Find(urun.UrunID);

                if (eskiUrun == null)
                {
                    return HttpNotFound();
                }

                eskiUrun.UrunAdi = urun.UrunAdi;
                eskiUrun.Marka = urun.Marka;
                eskiUrun.Aciklama = urun.Aciklama;
                eskiUrun.KategoriID = urun.KategoriID;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(
                db.Kategoriler.ToList(),
                "KategoriID", "KategoriAdi", urun.KategoriID);

            return View(urun);
        }

        [HttpPost]
        public ActionResult FavoriyeEkle(int id)
        {
            Favoriler favori = new Favoriler();

            favori.KullaniciID = 1;
            favori.UrunID = id;

            db.Favoriler.Add(favori);
            db.SaveChanges();

            return RedirectToAction("Favorilerim");
        }

        public ActionResult Favorilerim()
        {
            var liste = (from f in db.Favoriler
                         join u in db.Urunler
                         on f.UrunID equals u.UrunID
                         where f.KullaniciID == 1
                         select u).ToList();

            return View(liste);
        }
    }
}