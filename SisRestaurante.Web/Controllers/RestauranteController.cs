using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SisRestaurante.Domain;
using SisRestaurante.Domain.Context;
using SisRestaurante.Entity;
using SisRestaurante.Web.Models;

namespace SisRestaurante.Web.Controllers
{
    public class RestauranteController : Controller
    {
        private RestauranteRepository repRestaurante = new RestauranteRepository();
        private PratoRepository pratoRep = new PratoRepository();
        // GET: Restaurante
        public ActionResult Index()
        {
            //var restaurantes = repRestaurante.RetornaTodos().ToList();
            return View(FiltroRestaurante());
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult ListarRestaurante(string nome)
        {
            return PartialView("ListaRestaurantes", FiltroRestaurante(nome));
        }

        public List<RestauranteView> FiltroRestaurante(string nome = null)
        {
            List<Restaurante> restaurantes = repRestaurante.RetornaTodos().ToList();

            if (nome != null)
            {
                restaurantes = restaurantes.Where(r => r.Nome.ToUpper().Contains((nome.ToUpper()))).ToList();
            }
            var rest = new List<RestauranteView>();

            foreach (var item in restaurantes)
            {
                RestauranteView r = new RestauranteView()
                {
                    Nome = item.Nome
                };
                rest.Add(r);
            }

            return rest;
        }

        // GET: Restaurante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = repRestaurante.Procurar(id);

            Restaurante rest = new Restaurante()
            {
                Nome = restaurante.Nome,
                RestauranteId = restaurante.RestauranteId
            };

            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // GET: Restaurante/Create
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestauranteId,Nome")] RestauranteView restaurante)
        {
            if (ModelState.IsValid)
            {
                Restaurante rest = new Restaurante()
                {
                    Nome = restaurante.Nome
                };
                     
                repRestaurante.Inserir(rest);
                repRestaurante.SalvarTodos();

                ViewBag.Mensagem = "Restaurante Cadastrado com Sucesso!";

                return RedirectToAction("Index");
            }
            
            return View(restaurante);
        }

        // GET: Restaurante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = repRestaurante.Procurar(id);

            RestauranteView rest = new RestauranteView()
            {
                Nome = restaurante.Nome,
                RestauranteId = restaurante.RestauranteId
            };

            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // POST: Restaurante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestauranteId,Nome")] RestauranteView restaurante)
        {
            if (ModelState.IsValid)
            {
                Restaurante rest = new Restaurante()
                {
                    Nome = restaurante.Nome,
                    RestauranteId = restaurante.RestauranteId
                };

                repRestaurante.Alterar(rest);
                repRestaurante.SalvarTodos();

                return RedirectToAction("Index");
            }
            return View(restaurante);
        }

        // GET: Restaurante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = repRestaurante.Procurar(id);

            RestauranteView rest = new RestauranteView()
            {
                Nome = restaurante.Nome,
                RestauranteId = restaurante.RestauranteId
            };

            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // POST: Restaurante/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = repRestaurante.Procurar(id);

            var pratos = pratoRep.RetornaTodos().Where(p => p.Restaurante.RestauranteId == restaurante.RestauranteId).ToList();

            if (pratos.Count > 0)
            {
                foreach (var item in pratos)
                {
                    pratoRep.Excluir(p => p == item);
                }
                pratoRep.SalvarTodos();
            }

            repRestaurante.Excluir(r => r == restaurante);
            repRestaurante.SalvarTodos();

            ViewBag.Mensagem = "Restaurante excluido com Sucesso!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repRestaurante.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
