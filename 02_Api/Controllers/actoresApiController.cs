using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _04_Data.Datos;

namespace _02_Api.Controllers
{
    public class actoresApiController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/actoresApi
        public IQueryable<actor> Getactor()
        {
            return db.actor;
        }

        // GET: api/actoresApi/5
        [ResponseType(typeof(actor))]
        public IHttpActionResult Getactor(int id)
        {
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }
        [ResponseType(typeof(actor))]
        public IHttpActionResult Getactor(int? id, int? siguiente)
        {
            actor actorTabla = null;
            if (siguiente == null)
            {
                actorTabla = db.actor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    actorTabla = db.actor.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<actor> actorTablas = db.actor.Where(x => x.id < id.Value).ToList();
                    if (actorTablas != null && actorTablas.Count() > 0)
                    {
                        int? idactor = actorTablas.Max(x => x.id);
                        actorTabla = db.actor.Where(x => x.id == idactor.Value).FirstOrDefault();
                    }
                }
            }
            if (actorTabla == null)
            {
                actorTabla = db.actor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            actor actor = new actor();
            actor.id = actorTabla.id;
            actor.actor_principal = actorTabla.actor_principal;
            actor.fecha_nacimiento = actorTabla.fecha_nacimiento;
            actor.años_activo = actorTabla.años_activo;
            actor.ocupacion = actorTabla.ocupacion;
            actor.premios = actorTabla.premios;

            return Ok(actor);
        }

        // PUT: api/actoresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putactor(int id, actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.id)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!actorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/actoresApi
        [ResponseType(typeof(actor))]
        public IHttpActionResult Postactor(actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.actor.Add(actor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actor.id }, actor);
        }

        // DELETE: api/actoresApi/5
        [ResponseType(typeof(actor))]
        public IHttpActionResult Deleteactor(int id)
        {
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.actor.Remove(actor);
            db.SaveChanges();

            return Ok(actor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool actorExists(int id)
        {
            return db.actor.Count(e => e.id == id) > 0;
        }
    }
}