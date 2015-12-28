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
    public class TraductionRisquesController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: TraductionRisques
        public ActionResult Index()
        {
            var traductionRisques = db.TraductionRisques.Include(t => t.Langue).Include(t => t.Risque);
            return View(traductionRisques.ToList());
        }

        // GET: TraductionRisques/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionRisque traductionRisque = db.TraductionRisques.Find(id);
            if (traductionRisque == null)
            {
                return HttpNotFound();
            }
            return View(traductionRisque);
        }

        // GET: TraductionRisques/Create
        public ActionResult Create()
        {
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation");
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle");
            return View();
        }

        // POST: TraductionRisques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTraduction,DesRisLgue,libelle,codeLangue")] TraductionRisque traductionRisque)
        {
            if (ModelState.IsValid)
            {
                db.TraductionRisques.Add(traductionRisque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionRisque.codeLangue);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", traductionRisque.libelle);
            return View(traductionRisque);
        }

        // GET: TraductionRisques/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionRisque traductionRisque = db.TraductionRisques.Find(id);
            if (traductionRisque == null)
            {
                return HttpNotFound();
            }
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionRisque.codeLangue);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", traductionRisque.libelle);
            return View(traductionRisque);
        }

        // POST: TraductionRisques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTraduction,DesRisLgue,libelle,codeLangue")] TraductionRisque traductionRisque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traductionRisque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionRisque.codeLangue);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", traductionRisque.libelle);
            return View(traductionRisque);
        }

        // GET: TraductionRisques/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionRisque traductionRisque = db.TraductionRisques.Find(id);
            if (traductionRisque == null)
            {
                return HttpNotFound();
            }
            return View(traductionRisque);
        }

        // POST: TraductionRisques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TraductionRisque traductionRisque = db.TraductionRisques.Find(id);
            db.TraductionRisques.Remove(traductionRisque);
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
