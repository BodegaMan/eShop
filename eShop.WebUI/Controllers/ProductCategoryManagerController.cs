using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.Core.Contracts;
using eShop.Core.Models;
using eShop.DataAccess.InMemory;

namespace eShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        //ProductCategoryRepository context;
        //InMemoryRepository<ProductCategory> context;
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            //context = new InMemoryRepository<ProductCategory>();
            this.context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productcategories = context.Collection().ToList();
            return View(productcategories);
        }

        public ActionResult Create()
        {
            ProductCategory productcategory = new ProductCategory();
            return View(productcategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productcategory);
            }
            else
            {
                context.Insert(productcategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productcategory = context.Find(Id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory, string Id)
        {
            ProductCategory productcategoryToEdit = context.Find(Id);
            if (productcategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productcategory);
                }

                productcategoryToEdit.Category = productcategory.Category;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }

}