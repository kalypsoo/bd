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
    public class TraductionExamsController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: TraductionExams
        public ActionResult Index()
        {
            var traductionExams = db.TraductionExams.Include(t => t.Examan).Include(t => t.Langue);
            return View(traductionExams.ToList());
        }

        // GET: TraductionExams/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionExam traductionExam = db.TraductionExams.Find(id);
            if (traductionExam == null)
            {
                return HttpNotFound();
            }
            return View(traductionExam);
        }

        // GET: TraductionExams/Create
        public ActionResult Create()
        {
            ViewBag.libelle = new SelectList(db.Examen, "libelle", "periodicite");
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation");
            return View();
        }

        // POST: TraductionExams/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTraduction,DesExamLgue,codeLangue,libelle")] TraductionExam traductionExam)
        {
            if (ModelState.IsValid)
            {
                db.TraductionExams.Add(traductionExam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.libelle = new SelectList(db.Examen, "libelle", "periodicite", traductionExam.libelle);
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionExam.codeLangue);
            return View(traductionExam);
        }

        // GET: TraductionExams/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionExam traductionExam = db.TraductionExams.Find(id);
            if (traductionExam == null)
            {
                return HttpNotFound();
            }
            ViewBag.libelle = new SelectList(db.Examen, "libelle", "periodicite", traductionExam.libelle);
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionExam.codeLangue);
            return View(traductionExam);
        }

        // POST: TraductionExams/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTraduction,DesExamLgue,codeLangue,libelle")] TraductionExam traductionExam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traductionExam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.libelle = new SelectList(db.Examen, "libelle", "periodicite", traductionExam.libelle);
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionExam.codeLangue);
            return View(traductionExam);
        }

        // GET: TraductionExams/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionExam traductionExam = db.TraductionExams.Find(id);
            if (traductionExam == null)
            {
                return HttpNotFound();
            }
            return View(traductionExam);
        }

        // POST: TraductionExams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TraductionExam traductionExam = db.TraductionExams.Find(id);
            db.TraductionExams.Remove(traductionExam);
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
