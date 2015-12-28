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
    public class ExpositionsController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: Expositions
        public ActionResult Index()
        {
            var expositions = db.Expositions.Include(e => e.Emploi).Include(e => e.Risque);
            return View(expositions.ToList().OrderBy(e => e.Emploi.Personne.fullName));
        }

        // GET: Expositions/Details/5
        public ActionResult Details(string libelle, decimal numero)
        {
            if (libelle == null || numero == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.Expositions
                                 where libelle == p.libelle && numero == p.numero
                                 select p).SingleOrDefault();
            Exposition exposition = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            return View(exposition);
        }

        // GET: Expositions/Create
        public ActionResult Create()
        {
            ViewBag.numero = new SelectList((from s in db.Emplois.ToList()
                            select new
                            {
                                numero = s.numero,
                                FullName = s.Personne.nom + " " + s.Personne.prenom
                            }).OrderBy(s => s.FullName),
                            "numero",
                            "FullName",
                            null);
            ViewBag.libelle = new SelectList(db.Risques.OrderBy(s => s.libelle), "libelle", "libelle");
            return View();
        }

        // POST: Expositions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numero,libelle,Commentaire")] Exposition exposition)
        {
            var resultatQuery = (from p in db.Expositions
                                 where exposition.numero == p.numero && exposition.libelle == p.libelle
                                 select p).SingleOrDefault();

            if (resultatQuery == null && exposition.numero != 0 && exposition.libelle != null)
            {
                if (ModelState.IsValid)
                {
                    db.Expositions.Add(exposition);
                    db.SaveChanges();
                    TempData["Succes"] = "Exposition ajoutée !";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.numero = new SelectList((from s in db.Emplois.ToList()
                                             select new
                                             {
                                                 numero = s.numero,
                                                 FullName = s.Personne.nom + " " + s.Personne.prenom
                                             }),
                            "numero",
                            "FullName",
                            null);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", exposition.libelle);
            ModelState.AddModelError("", "Ce risque pour cet employe existe déjà");
            return View(exposition);
        }

        // GET: Expositions/Edit/5
        public ActionResult Edit(string libelle, decimal numero)
        {
            if (libelle == null || numero == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.Expositions
                                 where libelle == p.libelle && numero == p.numero
                                 select p).SingleOrDefault();
            Exposition exposition = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.numero = new SelectList(db.Emplois, "numero", "code", exposition.numero);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", exposition.libelle);
            return View(exposition);
        }

        // POST: Expositions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "numero,libelle,Commentaire")] Exposition exposition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exposition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.numero = new SelectList(db.Emplois, "numero", "code", exposition.numero);
            ViewBag.libelle = new SelectList(db.Risques, "libelle", "libelle", exposition.libelle);
            return View(exposition);
        }

        // GET: Expositions/Delete/5
        public ActionResult Delete(string libelle,decimal numero)
        {
            if (libelle == null || numero == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultatQuery = (from p in db.Expositions
                                 where libelle == p.libelle && numero == p.numero
                                 select p).SingleOrDefault();
            Exposition exposition = resultatQuery;
            if (resultatQuery == null)
            {
                return HttpNotFound();
            }
            return View(exposition);
        }

      
        // POST: Expositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string libelle, decimal numero)
        {
            var resultatQuery = (from p in db.Expositions
                                 where libelle == p.libelle && numero == p.numero
                                 select p).SingleOrDefault();
            Exposition exposition = resultatQuery;
            db.Expositions.Remove(exposition);
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
