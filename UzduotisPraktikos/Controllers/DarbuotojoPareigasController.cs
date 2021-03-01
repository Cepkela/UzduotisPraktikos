using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UzduotisPraktikos.Context;

namespace UzduotisPraktikos.Controllers
{
    public class DarbuotojoPareigasController : Controller
    {
        private PraktikosDarbuotojaiEntities db = new PraktikosDarbuotojaiEntities();

        // GET: DarbuotojoPareigas
        public ActionResult Index()
        {
            var darbuotojoPareigas = db.DarbuotojoPareigas.Include(d => d.Darbuotojai).Include(d => d.Pareigo);
            return View(darbuotojoPareigas.ToList());
        }

        // GET: DarbuotojoPareigas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DarbuotojoPareiga darbuotojoPareiga = db.DarbuotojoPareigas.Find(id);
            if (darbuotojoPareiga == null)
            {
                return HttpNotFound();
            }
            return View(darbuotojoPareiga);
        }

        // GET: DarbuotojoPareigas/Create
        public ActionResult Create()
        {
            ViewBag.DarbuotojasID = new SelectList(db.Darbuotojais, "ID", "Vardas");
            ViewBag.PareigosID = new SelectList(db.Pareigos, "PareigosID", "Pavadinimas");
            return View();
        }

        // POST: DarbuotojoPareigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PareigosID,DarbuotojasID")] DarbuotojoPareiga darbuotojoPareiga)
        {
            var res = db.Pareigos.ToList();
            if (ModelState.IsValid)
            {
                db.DarbuotojoPareigas.Add(darbuotojoPareiga);
                db.SaveChanges();
                return RedirectToAction("Index", "Darbuotojais", null);
            }

            ViewBag.DarbuotojasID = new SelectList(db.Darbuotojais, "ID", "Vardas", darbuotojoPareiga.DarbuotojasID);
            ViewBag.PareigosID = new SelectList(db.Pareigos, "PareigosID", "Pavadinimas", darbuotojoPareiga.PareigosID);
            return View(darbuotojoPareiga);
        }

        // GET: DarbuotojoPareigas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DarbuotojoPareiga darbuotojoPareiga = db.DarbuotojoPareigas.Find(id);
            if (darbuotojoPareiga == null)
            {
                return HttpNotFound();
            }
            ViewBag.DarbuotojasID = new SelectList(db.Darbuotojais, "ID", "Vardas", darbuotojoPareiga.DarbuotojasID);
            ViewBag.PareigosID = new SelectList(db.Pareigos, "PareigosID", "Pavadinimas", darbuotojoPareiga.PareigosID);
            return View(darbuotojoPareiga);
        }

        // POST: DarbuotojoPareigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PareigosID,DarbuotojasID")] DarbuotojoPareiga darbuotojoPareiga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(darbuotojoPareiga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DarbuotojasID = new SelectList(db.Darbuotojais, "ID", "Vardas", darbuotojoPareiga.DarbuotojasID);
            ViewBag.PareigosID = new SelectList(db.Pareigos, "PareigosID", "Pavadinimas", darbuotojoPareiga.PareigosID);
            return View(darbuotojoPareiga);
        }

        // GET: DarbuotojoPareigas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DarbuotojoPareiga darbuotojoPareiga = db.DarbuotojoPareigas.Find(id);
            if (darbuotojoPareiga == null)
            {
                return HttpNotFound();
            }
            return View(darbuotojoPareiga);
        }

        // POST: DarbuotojoPareigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DarbuotojoPareiga darbuotojoPareiga = db.DarbuotojoPareigas.Find(id);
            db.DarbuotojoPareigas.Remove(darbuotojoPareiga);
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
