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
    public class ExamRisquesController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: ExamRisques
        public ActionResult Index()
        {
            var examRisques = db.ExamRisques.Include(e => e.Examan).Include(e => e.Risque);
            return View(examRisques.ToList());
        }

        // GET: ExamRisques/Details/5
        public ActionResult Details(string libelleRisque, string libelleExamen)
        {
            if (libelleRisque == null || libelleExamen == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.ExamRisques
                                 where libelleRisque == p.libelle && libelleExamen == p.E_E_libelle
                                 select p).SingleOrDefault();
            ExamRisque examRisque = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            return View(examRisque);
        }

        // GET: ExamRisques/Create
        public ActionResult Create()
        {
            ViewBag.E_E_libelle = new SelectList(db.Examen, "libelle", "libelle");
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle");
            return View();
        }

        // POST: ExamRisques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "E_E_libelle,libelle,Commentaire")] ExamRisque examRisque)
        {
            var resultatQuery = (from p in db.ExamRisques
                                 where examRisque.E_E_libelle == p.E_E_libelle && examRisque.libelle == p.libelle
                                 select p).SingleOrDefault();

            if (resultatQuery == null)
            {
                if (ModelState.IsValid && examRisque.libelle != null && examRisque.E_E_libelle != null)
                {
                    db.ExamRisques.Add(examRisque);
                    db.SaveChanges();
                    TempData["Succes"] = "Examen-risque ajouté !";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.E_E_libelle = new SelectList(db.Examen, "libelle", "periodicite", examRisque.E_E_libelle);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", examRisque.libelle);
            return View(examRisque);
        }

        // GET: ExamRisques/Edit/5
        public ActionResult Edit(string libelleRisque, string libelleExamen)
        {
            if (libelleRisque == null || libelleExamen == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.ExamRisques
                                 where libelleRisque == p.libelle && libelleExamen == p.E_E_libelle
                                 select p).SingleOrDefault();
            ExamRisque examRisque = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.E_E_libelle = new SelectList(db.Examen, "libelle", "periodicite", examRisque.E_E_libelle);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", examRisque.libelle);
            return View(examRisque);
        }

        // POST: ExamRisques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "E_E_libelle,libelle,Commentaire")] ExamRisque examRisque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examRisque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.E_E_libelle = new SelectList(db.Examen, "libelle", "periodicite", examRisque.E_E_libelle);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", examRisque.libelle);
            return View(examRisque);
        }

        // GET: ExamRisques/Delete/5
        public ActionResult Delete(string libelleRisque, string libelleExamen)
        {
            if (libelleRisque == null || libelleExamen == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.ExamRisques
                                 where libelleRisque == p.libelle && libelleExamen == p.E_E_libelle
                                 select p).SingleOrDefault();
            ExamRisque examRisque = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            return View(examRisque);
        }

        // POST: ExamRisques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string libelleRisque, string libelleExamen)
        {
            if (libelleRisque == null || libelleExamen == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.ExamRisques
                                 where libelleRisque == p.libelle && libelleExamen == p.E_E_libelle
                                 select p).SingleOrDefault();
            ExamRisque examRisque = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            db.ExamRisques.Remove(examRisque);
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
