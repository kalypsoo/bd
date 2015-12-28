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
    public class LanguesController : Controller
    {
        private DBIG3A1Entities db = new DBIG3A1Entities();

        // GET: Langues
        public ActionResult Index()
        {
            return View(db.Langues.ToList());
        }

        // GET: Langues/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langue langue = db.Langues.Find(id);
            if (langue == null)
            {
                return HttpNotFound();
            }
            return View(langue);
        }

        // GET: Langues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Langues/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeLangue,designation")] Langue langue)
        {
            if (ModelState.IsValid)
            {
                db.Langues.Add(langue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(langue);
        }

        // GET: Langues/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langue langue = db.Langues.Find(id);
            if (langue == null)
            {
                return HttpNotFound();
            }
            return View(langue);
        }

        // POST: Langues/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeLangue,designation")] Langue langue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(langue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(langue);
        }

        // GET: Langues/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Langue langue = db.Langues.Find(id);
            if (langue == null)
            {
                return HttpNotFound();
            }
            return View(langue);
        }

        // POST: Langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Langue langue = db.Langues.Find(id);
            db.Langues.Remove(langue);
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
