using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class EmissorController : Controller
    {
        // GET: Emissor
        public ActionResult Index()
        {
            using (EmissorModel model = new EmissorModel())
            {
                List<Emissor> lista = model.Read();
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
            Emissor emissor = new Emissor();
            emissor.Nome = form["Nome"];

            using (EmissorModel model = new EmissorModel())
            {
                model.Create(emissor);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (EmissorModel model = new EmissorModel())
            {
                var emissor = model.ReadId(id);

                if (emissor == null)
                {
                    return HttpNotFound();
                }

                return View(emissor);
            }

        }

        [HttpPost]
        public ActionResult Edit(Emissor emissor)
        {
            using (EmissorModel model = new EmissorModel())
            {
                if (ModelState.IsValid)
                {
                    model.Update(emissor);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(emissor);
                }
            }
        }
        //POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            using (EmissorModel model = new EmissorModel())
            {
                var emissor = model.ReadId(id);

                if (emissor == null)
                {
                    return HttpNotFound();
                }

                return View(emissor);
            }
        }

        [HttpPost]
        public ActionResult Delete(Emissor emissor)
        {
            using (EmissorModel model = new EmissorModel())
            {
                if (ModelState.IsValid)
                {
                    model.Delete(emissor);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(emissor);
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (EmissorModel model = new EmissorModel())
            {
                var emissor = model.ReadId(id);

                if (emissor == null)
                {
                    return HttpNotFound();
                }

                return View(emissor);
            }

        }

    }
}

