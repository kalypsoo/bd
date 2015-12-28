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
    public class ExamenController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: Examen
        public ActionResult Index()
        {
            return View(db.Examen.ToList());
        }

        // GET: Examen/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examan examan = db.Examen.Find(id);
            if (examan == null)
            {
                return HttpNotFound();
            }
            return View(examan);
        }

        // GET: Examen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Examen/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "libelle,periodicite,numCompte,prixSoumis,prixNonSoumis")] Examan examan)
        {
            var resultatQuery = (from p in db.Examen
                                 where examan.libelle == p.libelle
                                 select p).SingleOrDefault();
            if (resultatQuery == null)
            {
                if (ModelState.IsValid)
                {
                    db.Examen.Add(examan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(examan);
        }

        // GET: Examen/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examan examan = db.Examen.Find(id);
            if (examan == null)
            {
                return HttpNotFound();
            }
            return View(examan);
        }

        // POST: Examen/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "libelle,periodicite,numCompte,prixSoumis,prixNonSoumis")] Examan examan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examan);
        }

        // GET: Examen/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examan examan = db.Examen.Find(id);
            if (examan == null)
            {
                return HttpNotFound();
            }
            return View(examan);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var resultatQuery = (from p in db.ExamRisques
                                  where id == p.E_E_libelle
                                  select p).ToList();
            if (resultatQuery.Count > 0)
            {
                foreach (ExamRisque examRisque in resultatQuery)
                {
                    db.ExamRisques.Remove(examRisque);
                    db.SaveChanges();
                }
            }
            Examan examan = db.Examen.Find(id);
            db.Examen.Remove(examan);
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
