﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _04_Data.Datos;

namespace _00_Mvc.Controllers
{
    public class actoresMvcController : Controller
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: actoresMvc
        public ActionResult Index()
        {
            return View(db.actor.ToList());
        }

        // GET: actoresMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: peliculasMvc/Details/5
        public ActionResult DetailsAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: actoresMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: actoresMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,actor_principal,fecha_nacimiento,años_activo,ocupacion,premios,foto")] actor actor)
        {
            if (ModelState.IsValid)
            {
                db.actor.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: actoresMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: actoresMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,actor_principal,fecha_nacimiento,años_activo,ocupacion,premios,foto")] actor actor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: actoresMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: actoresMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            actor actor = db.actor.Find(id);
            db.actor.Remove(actor);
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

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult _ActoresMvcPartialView(actor actor)
        {
            return View("_ActoresMvcPartialView", actor);
        }
    }
}
