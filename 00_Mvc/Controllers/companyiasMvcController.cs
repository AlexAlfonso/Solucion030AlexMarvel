using System;
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
    public class companyiasMvcController : Controller
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: companyiasMvc
        public ActionResult Index()
        {
            return View(db.companyia.ToList());
        }

        // GET: companyiasMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyia companyia = db.companyia.Find(id);
            if (companyia == null)
            {
                return HttpNotFound();
            }
            return View(companyia);
        }

        // GET: companyiasMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: companyiasMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,tipo,industria,sede_central,fundacion,fundador,logo")] companyia companyia)
        {
            if (ModelState.IsValid)
            {
                db.companyia.Add(companyia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyia);
        }

        // GET: companyiasMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyia companyia = db.companyia.Find(id);
            if (companyia == null)
            {
                return HttpNotFound();
            }
            return View(companyia);
        }

        // POST: companyiasMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,tipo,industria,sede_central,fundacion,fundador,logo")] companyia companyia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyia);
        }

        // GET: companyiasMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            companyia companyia = db.companyia.Find(id);
            if (companyia == null)
            {
                return HttpNotFound();
            }
            return View(companyia);
        }

        // POST: companyiasMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            companyia companyia = db.companyia.Find(id);
            db.companyia.Remove(companyia);
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
