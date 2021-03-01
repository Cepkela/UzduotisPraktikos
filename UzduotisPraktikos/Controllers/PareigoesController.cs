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
    public class PareigoesController : Controller
    {
        private PraktikosDarbuotojaiEntities db = new PraktikosDarbuotojaiEntities();

        // GET: Pareigoes
        public ActionResult Index()
        {
            return View(db.Pareigos.ToList());
        }

        // GET: Pareigoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pareigo pareigo = db.Pareigos.Find(id);
            if (pareigo == null)
            {
                return HttpNotFound();
            }
            return View(pareigo);
        }

        // GET: Pareigoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pareigoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PareigosID,Pavadinimas")] Pareigo pareigo)
        {
            if (ModelState.IsValid)
            {
                db.Pareigos.Add(pareigo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pareigo);
        }

        public ActionResult DarbuotojoPareigos(int id)
        {
            var res = db.DarbuotojoPareigas.ToList();
            var red = db.Pareigos.ToList();
            List<Pareigo> pareigos = new List<Pareigo>();
            foreach (var item in res)
            {
                if (item.DarbuotojasID == id)
                {
                    foreach (var items in red)
                    {
                        if (item.PareigosID == items.PareigosID)
                        {
                            pareigos.Add(items);
                        }
                    }
                }
            }
            return View("Index", pareigos);
        }

        // GET: Pareigoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pareigo pareigo = db.Pareigos.Find(id);
            if (pareigo == null)
            {
                return HttpNotFound();
            }
            return View(pareigo);
        }

        // POST: Pareigoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PareigosID,Pavadinimas")] Pareigo pareigo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pareigo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pareigo);
        }
        public ActionResult PareigosPriskyrimas()
        {
            return View();
        }
        // GET: Pareigoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pareigo pareigo = db.Pareigos.Find(id);
            if (pareigo == null)
            {
                return HttpNotFound();
            }
            return View(pareigo);
        }

        // POST: Pareigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pareigo pareigo = db.Pareigos.Find(id);
            db.Pareigos.Remove(pareigo);
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
