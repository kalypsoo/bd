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
    public class TraductionProfsController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: TraductionProfs
        public ActionResult Index()
        {
            var traductionProfs = db.TraductionProfs.Include(t => t.Langue).Include(t => t.Profession);
            return View(traductionProfs.ToList());
        }

        // GET: TraductionProfs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionProf traductionProf = db.TraductionProfs.Find(id);
            if (traductionProf == null)
            {
                return HttpNotFound();
            }
            return View(traductionProf);
        }

        // GET: TraductionProfs/Create
        public ActionResult Create()
        {
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation");
            ViewBag.code = new SelectList(db.Professions, "code", "code");
            return View();
        }

        // POST: TraductionProfs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTraduction,desProfTrad,code,codeLangue")] TraductionProf traductionProf)
        {
            if (ModelState.IsValid)
            {
                db.TraductionProfs.Add(traductionProf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionProf.codeLangue);
            ViewBag.code = new SelectList(db.Professions, "code", "code", traductionProf.code);
            return View(traductionProf);
        }

        // GET: TraductionProfs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionProf traductionProf = db.TraductionProfs.Find(id);
            if (traductionProf == null)
            {
                return HttpNotFound();
            }
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionProf.codeLangue);
            ViewBag.code = new SelectList(db.Professions, "code", "code", traductionProf.code);
            return View(traductionProf);
        }

        // POST: TraductionProfs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTraduction,desProfTrad,code,codeLangue")] TraductionProf traductionProf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traductionProf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codeLangue = new SelectList(db.Langues, "codeLangue", "designation", traductionProf.codeLangue);
            ViewBag.code = new SelectList(db.Professions, "code", "code", traductionProf.code);
            return View(traductionProf);
        }

        // GET: TraductionProfs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraductionProf traductionProf = db.TraductionProfs.Find(id);
            if (traductionProf == null)
            {
                return HttpNotFound();
            }
            return View(traductionProf);
        }

        // POST: TraductionProfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TraductionProf traductionProf = db.TraductionProfs.Find(id);
            db.TraductionProfs.Remove(traductionProf);
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
