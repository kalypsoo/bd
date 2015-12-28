using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetBD.Models;

namespace ProjetBD.Controllers
{
    public class RisquesController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: Risques
        public ActionResult Index()
        {
            return View(db.Risques.ToList());
        }

        // GET: Risques/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // GET: Risques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Risques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "libelle")] Risque risque)
        {
            var resultatQuery = (from p in db.Risques
                                 where risque.libelle == p.libelle
                                 select p).SingleOrDefault();

            if (resultatQuery == null)
            {
                if (ModelState.IsValid)
                {
                    db.Risques.Add(risque);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Ce risque existe déjà");
            return View(risque);
        }

        // GET: Risques/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "libelle")] Risque risque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(risque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(risque);
        }

        // GET: Risques/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var resultatQuery = (from p in db.Expositions
                                 where id == p.libelle
                                 select p).ToList();
            if (resultatQuery.Count > 0)
            {
                foreach (Exposition exposition in resultatQuery)
                {
                    db.Expositions.Remove(exposition);
                    db.SaveChanges();
                }
            }

            var resultatQuery1 = (from p in db.ExamRisques
                                 where id == p.libelle
                                 select p).ToList();
            if (resultatQuery1.Count > 0)
            {
                foreach (ExamRisque examRisque in resultatQuery1)
                {
                    db.ExamRisques.Remove(examRisque);
                    db.SaveChanges();
                }
            }

            Risque risque = db.Risques.Find(id);
            db.Risques.Remove(risque);
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
