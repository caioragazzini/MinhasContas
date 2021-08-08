using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                List<Categoria> lista = model.Read();
                return View(lista);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Categoria categoria = new Categoria();
            categoria.Nome = form["Nome"];
            
            using (CategoriaModel model = new CategoriaModel())
            {
                model.Create(categoria);
                return RedirectToAction("Index");
            }
        }
       
        public ActionResult Edit(int id)
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                var categoria = model.ReadId(id);

                if (categoria == null)
                {
                    return HttpNotFound();
                }

                return View(categoria);
            }

        }

        [HttpPost]
        public ActionResult Edit(Categoria categoria)
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                if (ModelState.IsValid)
                {
                    model.Update(categoria);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoria);
                }
            }
        }
        //POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                var categoria = model.ReadId(id);

                if (categoria == null)
                {
                    return HttpNotFound();
                }

                return View(categoria);
            }
        }

        [HttpPost]
        public ActionResult Delete (Categoria categoria)
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                if (ModelState.IsValid)
                {
                    model.Delete(categoria);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoria);
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (CategoriaModel model = new CategoriaModel())
            {
                var categoria = model.ReadId(id);

                if (categoria == null)
                {
                    return HttpNotFound();
                }

                return View(categoria);
            }




        }




    }
}

