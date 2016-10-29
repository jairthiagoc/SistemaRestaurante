using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SisRestaurante.Domain;
using SisRestaurante.Domain.Context;
using SisRestaurante.Entity;
using SisRestaurante.Web.Models;

namespace SisRestaurante.Web.Controllers
{
    public class PratoController : Controller
    {
        private PratoRepository pratorep = new PratoRepository();
        private RestauranteRepository restrep = new RestauranteRepository();

        // GET: Prato
        public ActionResult Index()
        {
            var lspratosview = new List<PratoViewModel>();
            var lsPratos = pratorep.RetornaTodos().Include(p => p.Restaurante);

            if (lsPratos.Count() > 0)
            {
                foreach (var item in lsPratos)
                {
                    PratoViewModel prato = new PratoViewModel()
                    {
                        Nome = item.Nome,
                        Preco = item.Preco,
                        RestauranteId = item.RestauranteId,
                        PratoId = item.PratoId,
                        Restaurante = new RestauranteViewModel()
                        {
                            Nome = item.Restaurante.Nome
                        },
                    };

                    lspratosview.Add(prato);
                }
                return View(lspratosview);
            }
            return View(lspratosview);
        }

        // GET: Prato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = pratorep.Procurar(id);

            PratoViewModel pratoview = new PratoViewModel()
            {
                Nome = prato.Nome,
                Preco = prato.Preco,
                RestauranteId = prato.RestauranteId
            };

            if (pratoview == null)
            {
                return HttpNotFound();
            }
            return View(pratoview);
        }

        // GET: Prato/Create
        public ActionResult Create()
        {
            ViewBag.Restaurante = new SelectList(restrep.RetornaTodos(), "RestauranteId", "Nome");
            return View();
        }

        // POST: Prato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PratoId,Nome,Preco,RestauranteId")] PratoViewModel pratoview)
        {
            if (ModelState.IsValid)
            {
                Prato prato = new Prato();
                prato.Nome = pratoview.Nome;
                prato.Preco = pratoview.Preco;
                prato.RestauranteId = pratoview.RestauranteId;

                pratorep.Inserir(prato);
                pratorep.SalvarTodos();

                return RedirectToAction("Index");
            }

            ViewBag.Restaurante = new SelectList(restrep.RetornaTodos(), "RestauranteId", "Nome");
            return View(pratoview);
        }

        // GET: Prato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = pratorep.Procurar(id);

            var dados = String.Format("{0:0.##}", prato.Preco);

            PratoViewModel pratoview = new PratoViewModel()
            {
                PratoId = prato.PratoId,
                Nome = prato.Nome.Trim(),
                Preco = Convert.ToDecimal(dados),
                RestauranteId = prato.RestauranteId
            };

            if (pratoview == null)
            {
                return HttpNotFound();
            }
            ViewBag.Restaurante = new SelectList(restrep.RetornaTodos(), "RestauranteId", "Nome", prato.RestauranteId);
            return View(pratoview);
        }

        // POST: Prato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PratoId,Nome,Preco,RestauranteId")] PratoViewModel pratoview)
        {
            if (ModelState.IsValid)
            {
                Prato prato = new Prato();
                prato.PratoId = pratoview.PratoId;
                prato.Nome = pratoview.Nome;
                prato.Preco = pratoview.Preco;
                prato.RestauranteId = pratoview.RestauranteId;
                
                pratorep.Alterar(prato);
                pratorep.SalvarTodos();

                return RedirectToAction("Index");
            }
            return View(pratoview);
        }

        // GET: Prato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = pratorep.Procurar(id);

            PratoViewModel pratoview = new PratoViewModel()
            {
                PratoId = prato.PratoId,
                Nome = prato.Nome,
                Preco = prato.Preco,
                RestauranteId = prato.RestauranteId
            };

            if (pratoview == null)
            {
                return HttpNotFound();
            }

            ViewBag.Restaurante = new SelectList(restrep.RetornaTodos(), "RestauranteId", "Nome", prato.RestauranteId);
            return View(pratoview);
        }

        // POST: Prato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prato prato = pratorep.Procurar(id);
            pratorep.Excluir(p => p.PratoId == id);
            pratorep.SalvarTodos();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pratorep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
