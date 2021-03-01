using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UzduotisPraktikos.Context;

namespace UzduotisPraktikos.Controllers
{
    public class DarbuotojaisController : Controller
    {
        private PraktikosDarbuotojaiEntities db = new PraktikosDarbuotojaiEntities();

        // GET: Darbuotojai
        public ActionResult Index()
        {
            return View(db.Darbuotojais.ToList());
        }
        public ActionResult DarbuotojuSarasas()
        {
            var res = db.Darbuotojais.ToList();
            List<Darbuotojai> darb = new List<Darbuotojai>();
            foreach (var item in res)
            {
                if (item.Aktyvus == true)
                {
                    darb.Add(item);
                }
            }
            return View("Index", darb);
        }
        public ActionResult AktyvuotiDarbuotoja(int id)
        {
            var res = db.Darbuotojais.Where(x => x.ID == id).First();
            res.Aktyvus = true;
            //dbObjektas.Darbuotojais.Remove(res);
            db.SaveChanges();

            var list = db.Darbuotojais.ToList();
            List<Darbuotojai> darb = new List<Darbuotojai>();
            foreach (var item in list)
            {
                if (item.Aktyvus == true)
                {
                    darb.Add(item);
                }
            }
            return View("Index", darb);
        }
        public ActionResult DeaktyvuotiDarbuotoja(int id)
        {
            var red = db.DarbuotojoPareigas.ToList();
            var res = db.Darbuotojais.Where(x => x.ID == id).First();
            res.Aktyvus = false;
            foreach (var item in red)
            {
                if (res.ID == item.DarbuotojasID)
                {
                    db.DarbuotojoPareigas.Remove(item);
                    db.SaveChanges();
                }
            }
            //dbObjektas.Darbuotojais.Remove(res);
            db.SaveChanges();

            var list = db.Darbuotojais.ToList();

            List<Darbuotojai> darb = new List<Darbuotojai>();
            foreach (var item in list)
            {
                if (item.Aktyvus == true)
                {
                    darb.Add(item);
                }
            }
            return View("Index", darb);
        }
        public ActionResult NeaktyviuDarbuotojuSarasas()
        {
            var res = db.Darbuotojais.ToList();
            List<Darbuotojai> darb = new List<Darbuotojai>();
            foreach (var item in res)
            {
                if (item.Aktyvus == false)
                {
                    darb.Add(item);
                }
            }
            return View("Index", darb);
        }

        // GET: Darbuotojais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Darbuotojai darbuotojai = db.Darbuotojais.Find(id);
            if (darbuotojai == null)
            {
                return HttpNotFound();
            }
            return View(darbuotojai);
        }

        // GET: Darbuotojais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Darbuotojais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vardas,Pavarde,GimimoData,Adresas,Aktyvus")] Darbuotojai darbuotojai)
        {
            if (ModelState.IsValid)
            {
                db.Darbuotojais.Add(darbuotojai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(darbuotojai);
        }

        // GET: Darbuotojais/Edit/5
        public ActionResult Edit(int id)
        {
            Darbuotojai darbuotojai = db.Darbuotojais.Find(id);
            return View(darbuotojai);
        }

        // POST: Darbuotojais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vardas,Pavarde,GimimoData,Adresas,Aktyvus")] Darbuotojai darbuotojai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(darbuotojai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(darbuotojai);
        }

        // GET: Darbuotojais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Darbuotojai darbuotojai = db.Darbuotojais.Find(id);
            if (darbuotojai == null)
            {
                return HttpNotFound();
            }
            return View(darbuotojai);
        }

        // POST: Darbuotojais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Darbuotojai darbuotojai = db.Darbuotojais.Find(id);
            db.Darbuotojais.Remove(darbuotojai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
