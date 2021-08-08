using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class FaturaController : Controller
    {
        public ActionResult Index()
        {
            using (FaturaModel model = new FaturaModel())
            {
                var fatura = model.Read();
                return View(fatura);
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
            Fatura fatura = new Fatura();
            fatura.EmissorId = Convert.ToInt32(form["EmissorId"]);
            fatura.ValorConta = Convert.ToDecimal(form["ValorConta"]);
            fatura.DataFatura = Convert.ToDateTime(form["DataFatura"]);
            fatura.DataVencimento = Convert.ToDateTime(form["DataVencimento"]);
            fatura.Status = Convert.ToBoolean(form["Status"]);

            using (FaturaModel model = new FaturaModel())
            {
                model.Create(fatura);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (FaturaModel model = new FaturaModel())
            {
                var fatura = model.ReadId(id);

                if (fatura == null)
                {
                    return HttpNotFound();
                }

                return View(fatura);
            }

        }

        [HttpPost]
        public ActionResult Edit(Fatura fatura)
        {
            using (FaturaModel model = new FaturaModel())
            {
                if (ModelState.IsValid)
                {
                    model.Update(fatura);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(fatura);
                }
            }
        }
        //POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            using (FaturaModel model = new FaturaModel())
            {
                var fatura = model.ReadId(id);

                if (fatura == null)
                {
                    return HttpNotFound();
                }

                return View(fatura);
            }
        }

        [HttpPost]
        public ActionResult Delete(Fatura fatura)
        {
            using (FaturaModel model = new FaturaModel())
            {
                if (ModelState.IsValid)
                {
                    model.Delete(fatura);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(fatura);
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (FaturaModel model = new FaturaModel())
            {
                var fatura = model.ReadId(id);

                if (fatura == null)
                {
                    return HttpNotFound();
                }
                return View(fatura);
            }

        }

    }
}

