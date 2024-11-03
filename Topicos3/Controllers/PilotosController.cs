using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Topicos3.Models;

namespace Topicos3.Controllers
{
    public class PilotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pilotos
        public async Task<ActionResult> Index()
        {
            return View(await db.Pilotos.ToListAsync());
        }

        // GET: Pilotos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = await db.Pilotos.FindAsync(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // GET: Pilotos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pilotos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,DataNascimento,Cpf,TipoLicenca,TempoExperiencia,HorasVoo,UltimaAvaliacaoMedica,UltimoTreinamento")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Pilotos.Add(piloto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(piloto);
        }

        // GET: Pilotos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = await db.Pilotos.FindAsync(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Pilotos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,DataNascimento,Cpf,TipoLicenca,TempoExperiencia,HorasVoo,UltimaAvaliacaoMedica,UltimoTreinamento")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piloto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(piloto);
        }

        // GET: Pilotos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = await db.Pilotos.FindAsync(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Pilotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Piloto piloto = await db.Pilotos.FindAsync(id);
            db.Pilotos.Remove(piloto);
            await db.SaveChangesAsync();
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
