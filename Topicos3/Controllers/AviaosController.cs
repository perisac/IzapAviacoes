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
    public class AviaosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }

            base.OnActionExecuting(filterContext);
        }

        // GET: Aviaos
        public async Task<ActionResult> Index()
        {
            return View(await db.Avioes.ToListAsync());
        }

        // GET: Aviaos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = await db.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            return View(aviao);
        }

        // GET: Aviaos/Create
        public ActionResult Create()
        {
            ViewBag.TripulacaoId = new SelectList(db.Tripulacoes, "Id", "NomeTripulacao");
            return View();
        }

        // POST: Aviaos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Modelo,Autonomia,AnoFabricacao,Capacidade,VelocidadeMedia,HorasVoo,UltimaManutencao,TripulacaoId")] Aviao aviao)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Exibe ou registra cada mensagem de erro
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                // Busca e associa a Tripulação ao Avião
                var tripulacao = await db.Tripulacoes.FindAsync(aviao.TripulacaoId);

                if (tripulacao == null)
                {
                    ModelState.AddModelError("", "Tripulação não encontrada.");
                }
                else
                {
                    aviao.Tripulacao = tripulacao;
                }

                db.Avioes.Add(aviao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Preenche a ViewBag com a lista de Tripulações para a View
            ViewBag.TripulacaoId = new SelectList(db.Tripulacoes, "Id", "NomeTripulacao"); // Certifique-se de usar uma propriedade legível

            return View(aviao);
        }

        // GET: Aviaos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = await db.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            // No seu método Edit (GET)
            ViewBag.TripulacaoId = new SelectList(db.Tripulacoes, "Id", "NomeTripulacao", aviao.TripulacaoId);
            return View(aviao);
        }

        // POST: Aviaos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Modelo,Autonomia,AnoFabricacao,Capacidade,VelocidadeMedia,HorasVoo,UltimaManutencao,TripulacaoId")] Aviao aviao)
        {
            if (ModelState.IsValid)
            {

                // Busca a Tripulação associada
                var tripulacao = await db.Tripulacoes.FindAsync(aviao.TripulacaoId);
                if (tripulacao == null)
                {
                    // Adiciona um erro ao ModelState se a Tripulação não for encontrada
                    ModelState.AddModelError("", "Tripulação não encontrada.");
                    // Preenche o ViewBag novamente para a View
                    ViewBag.TripulacaoId = new SelectList(db.Tripulacoes, "Id", "NomeTripulacao", aviao.TripulacaoId);
                    return View(aviao); // Retorna a View sem salvar as alterações
                }

                // Associa a Tripulação ao Avião
                aviao.Tripulacao = tripulacao;

                // Marca o Avião como modificado e salva as alterações
                db.Entry(aviao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Se o ModelState não for válido, preenche o ViewBag novamente
            ViewBag.TripulacaoId = new SelectList(db.Tripulacoes, "Id", "NomeTripulacao", aviao.TripulacaoId);
            return View(aviao);
        }


        // GET: Aviaos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = await db.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            return View(aviao);
        }

        // POST: Aviaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aviao aviao = await db.Avioes.FindAsync(id);
            db.Avioes.Remove(aviao);
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
