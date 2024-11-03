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
    public class ComissariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comissarios
        public async Task<ActionResult> Index()
        {
            return View(await db.Comissarios.ToListAsync());
        }

        // GET: Comissarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissario comissario = await db.Comissarios.FindAsync(id);
            if (comissario == null)
            {
                return HttpNotFound();
            }
            return View(comissario);
        }

        // GET: Comissarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comissarios/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,DataNascimento,Cpf,AnosExperiencia,UltimoTreinamento")] Comissario comissario)
        {
            if (ModelState.IsValid)
            {
                db.Comissarios.Add(comissario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(comissario);
        }

        // GET: Comissarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissario comissario = await db.Comissarios.FindAsync(id);
            if (comissario == null)
            {
                return HttpNotFound();
            }
            return View(comissario);
        }

        // POST: Comissarios/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,DataNascimento,Cpf,AnosExperiencia,UltimoTreinamento")] Comissario comissario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comissario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comissario);
        }

        // GET: Comissarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissario comissario = await db.Comissarios.FindAsync(id);
            if (comissario == null)
            {
                return HttpNotFound();
            }
            return View(comissario);
        }

        // POST: Comissarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comissario comissario = await db.Comissarios.FindAsync(id);
            db.Comissarios.Remove(comissario);
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
