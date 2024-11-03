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
    public class RotasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rotas
        public async Task<ActionResult> Index()
        {
            return View(await db.Rotas.ToListAsync());
        }

        // GET: Rotas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rota rota = await db.Rotas.FindAsync(id);
            if (rota == null)
            {
                return HttpNotFound();
            }
            return View(rota);
        }

        // GET: Rotas/Create
        public ActionResult Create()
        {
            ViewBag.AviaoId = new SelectList(db.Avioes, "Id", "Modelo"); // Ajuste "Modelo" para o campo adequado
            return View();
        }

        // POST: Rotas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Origem,Destino,Distancia,TempoEstimado,AviaoId")] Rota rota)
        {
            if (ModelState.IsValid)
            {
                // Busca e associa o Avião à Rota
                var aviao = await db.Avioes.FindAsync(rota.AviaoId);
                if (aviao != null)
                {
                    rota.Aviao = aviao;
                }

                db.Rotas.Add(rota);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Preenche qualquer dado adicional necessário para a View
            ViewBag.AviaoId = new SelectList(db.Avioes, "Id", "Modelo", rota.AviaoId); // Ajuste "Modelo" para o campo adequado

            return View(rota);
        }

        // GET: Rotas/Edit/5
        // GET: Rotas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca a rota com o ID fornecido
            Rota rota = await db.Rotas.FindAsync(id);
            if (rota == null)
            {
                return HttpNotFound();
            }

            // Preenche o ViewBag com a lista de Aviões para a DropDownList
            ViewBag.AviaoId = new SelectList(db.Avioes, "Id", "Modelo", rota.AviaoId); // Ajuste "Modelo" para a propriedade correta

            return View(rota);
        }

        // POST: Rotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Origem,Destino,Distancia,TempoEstimado,AviaoId")] Rota rota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rota).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Se o ModelState não for válido, preenche novamente o ViewBag com a lista de Aviões
            ViewBag.AviaoId = new SelectList(db.Avioes, "Id", "Modelo", rota.AviaoId);

            return View(rota);
        }


        // GET: Rotas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rota rota = await db.Rotas.FindAsync(id);
            if (rota == null)
            {
                return HttpNotFound();
            }
            return View(rota);
        }

        // POST: Rotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rota rota = await db.Rotas.FindAsync(id);
            db.Rotas.Remove(rota);
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
