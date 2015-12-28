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
    public class EmploisController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: Emplois
        public ActionResult Index()
        {
            var emplois = db.Emplois.Include(e => e.Entreprise).Include(e => e.Personne).Include(e => e.Profession);
            return View(emplois.ToList());
        }

        // GET: Emplois/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // GET: Emplois/Create
        public ActionResult Create()
        {
            ViewBag.R_1_numero = new SelectList(db.Entreprises, "numero", "denomination");
            ViewBag.R_numero = new SelectList(db.Personnes, "numero", "nom");
            ViewBag.code = new SelectList(db.Professions, "code", "code");
            return View();
        }

        // POST: Emplois/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateEntree,dateSortie,numero,code,R_1_numero,R_numero")] Emploi emploi)
        {
            if (ModelState.IsValid)
            {
                db.Emplois.Add(emploi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.R_1_numero = new SelectList(db.Entreprises, "numero", "denomination", emploi.R_1_numero);
            ViewBag.R_numero = new SelectList(db.Personnes, "numero", "nom", emploi.R_numero);
            ViewBag.code = new SelectList(db.Professions, "code", "code", emploi.code);
            return View(emploi);
        }

        // GET: Emplois/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            ViewBag.R_1_numero = new SelectList(db.Entreprises, "numero", "denomination", emploi.R_1_numero);
            ViewBag.R_numero = new SelectList(db.Personnes, "numero", "nom", emploi.R_numero);
            ViewBag.code = new SelectList(db.Professions, "code", "code", emploi.code);
            return View(emploi);
        }

        // POST: Emplois/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateEntree,dateSortie,numero,code,R_1_numero,R_numero")] Emploi emploi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.R_1_numero = new SelectList(db.Entreprises, "numero", "denomination", emploi.R_1_numero);
            ViewBag.R_numero = new SelectList(db.Personnes, "numero", "nom", emploi.R_numero);
            ViewBag.code = new SelectList(db.Professions, "code", "code", emploi.code);
            return View(emploi);
        }

        // GET: Emplois/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // POST: Emplois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Emploi emploi = db.Emplois.Find(id);
            db.Emplois.Remove(emploi);
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
