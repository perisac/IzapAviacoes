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
    public class TripulacoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tripulacoes
        public async Task<ActionResult> Index()
        {
            var tripulacoes = db.Tripulacoes.Include(t => t.CoPiloto).Include(t => t.Piloto);
            return View(await tripulacoes.ToListAsync());
        }

        // GET: Tripulacoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tripulacao tripulacao = await db.Tripulacoes.FindAsync(id);
            if (tripulacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoPilotoId = new SelectList(db.Pilotos, "Id", "Nome");
            ViewBag.PilotoId = new SelectList(db.Pilotos, "Id", "Nome");
            ViewBag.comissarioIds = new SelectList(db.Comissarios, "Id", "Nome");
            return View(tripulacao);
        }

        // GET: Tripulacoes/Create
        public ActionResult Create()
        {
            ViewBag.CoPilotoId = new SelectList(db.Pilotos, "Id", "Nome");
            ViewBag.PilotoId = new SelectList(db.Pilotos, "Id", "Nome");
            ViewBag.comissarioIds = new SelectList(db.Comissarios, "Id", "Nome");
            return View();
        }

        // POST: Tripulacoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NomeTripulacao,PilotoId,CoPilotoId,Comissarios")] Tripulacao tripulacao, int[] comissarioIds)
        {
            if (ModelState.IsValid)
            {
                // Adiciona os comissários selecionados à tripulação
                if (comissarioIds != null)
                {
                    tripulacao.Comissarios = new List<Comissario>();
                    foreach (var id in comissarioIds)
                    {
                        var comissario = await db.Comissarios.FindAsync(id);
                        if (comissario != null)
                        {
                            tripulacao.Comissarios.Add(comissario);
                        }
                    }
                }

                db.Tripulacoes.Add(tripulacao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Preenchendo ViewBag, mesmo se o ModelState for inválido
            ViewBag.CoPilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.CoPilotoId);
            ViewBag.PilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.PilotoId);
            ViewBag.comissarioIds = new MultiSelectList(db.Comissarios, "Id", "Nome", comissarioIds); // Usando MultiSelectList

            return View(tripulacao);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca a rota com o ID fornecido
            Tripulacao tripulacao = await db.Tripulacoes.FindAsync(id);
            if (tripulacao == null)
            {
                return HttpNotFound();
            }

            var comissarioIds = tripulacao.Comissarios.Select(c => c.Id).ToArray();

            ViewBag.CoPilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.CoPilotoId);
            ViewBag.PilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.PilotoId);
            ViewBag.comissarioIds = new MultiSelectList(db.Comissarios, "Id", "Nome", comissarioIds); // Usando MultiSelectList

            return View(tripulacao);
        }
        // POST: Tripulacoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NomeTripulacao,PilotoId,CoPilotoId,Comissarios")] Tripulacao tripulacao, int[] comissarioIds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tripulacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CoPilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.CoPilotoId);
            ViewBag.PilotoId = new SelectList(db.Pilotos, "Id", "Nome", tripulacao.PilotoId);
            ViewBag.comissarioIds = new MultiSelectList(db.Comissarios, "Id", "Nome", comissarioIds); // Usando MultiSelectList
            return View(tripulacao);
        }

        // GET: Tripulacoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tripulacao = await db.Tripulacoes.FindAsync(id);
            if (tripulacao == null)
            {
                return HttpNotFound();
            }

            // Verifica se a Tripulacao está associada a algum Aviao
            var aviaoAssociado = db.Avioes.Any(a => a.TripulacaoId == id);
            if (aviaoAssociado)
            {
                // Adiciona uma mensagem de erro e retorna à View, sem excluir
                ModelState.AddModelError("", "Não é possível excluir a tripulação porque está associada a um avião.");
                return View(tripulacao);
            }

            return View(tripulacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var tripulacao = await db.Tripulacoes.FindAsync(id);

            // Verifica novamente antes de excluir
            var aviaoAssociado = db.Avioes.Any(a => a.TripulacaoId == id);
            if (aviaoAssociado)
            {
                ModelState.AddModelError("", "Não é possível excluir a tripulação porque está associada a um avião.");
                return View(tripulacao);
            }

            db.Tripulacoes.Remove(tripulacao);
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
