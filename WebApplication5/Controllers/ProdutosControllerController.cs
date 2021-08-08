using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ProdutosControllerController : Controller
    {
        // GET:Produtos
        public ActionResult Index()
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                List<clsProdutos> lista = model.Read();
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
            clsProdutos produto = new clsProdutos();
            produto.NomeProduto = form["NomeProduto"];
            produto.ValorProduto = Convert.ToDecimal(form["ValorProduto"]);
            produto.QtdeProduto = Convert.ToInt32(form["QtdeProduto"]);


            using (ProdutoModel model = new ProdutoModel())
            {
                model.Create(produto);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CreateProc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProc(FormCollection form)
        {
            clsProdutos produto = new clsProdutos();
            produto.NomeProduto = form["NomeProduto"];
            produto.ValorProduto = Convert.ToDecimal(form["ValorProduto"]);
            produto.QtdeProduto = Convert.ToInt32(form["QtdeProduto"]);
            using (ProdutoModel model = new ProdutoModel())
            {
                model.CreateProc(produto);
                return RedirectToAction("Index");
            }
        }



        public ActionResult Edit(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }




        }

        [HttpPost]
        public ActionResult Edit(clsProdutos produto)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                if (ModelState.IsValid)
                {
                    model.Update(produto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(produto);
                }
            }
        }


        //POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }




        }

        [HttpPost]
        public ActionResult Delete(clsProdutos produto)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                if (ModelState.IsValid)
                {
                    model.Delete(produto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(produto);
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }




        }




    }
}
