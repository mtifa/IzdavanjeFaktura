using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssuingInvoices;
using IssuingInvoices.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IssuingInvoices.Controllers
{
    public class ProductsController : Controller
    {
        private InvoicesModel db = new InvoicesModel();
        private List<int?> selectedIds = new List<int?>();
        private ApplicationUserManager userManager;

        public ProductsController()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        // GET: Products
        public ActionResult Index(string search)
        {
            return View(db.Products.Where(x => x.Description == search || search == null).ToList()); //return View(db.Products.ToList());
        }
        public ActionResult Add(int? id)
        {
            selectedIds.Add(id);
            return View();
        }
        [HttpPost]
        public ActionResult SubmitSelected(InvoicesModel model)
        {
            var selectedProducts = from x in db.Products
                                 where selectedIds.Contains(x.ProductId)
                                 select x;

            foreach (var product in selectedProducts)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}", product.Description));
                Invoice invoice = new Invoice();
                invoice.Products.Add(product);
                var user = userManager.FindById(User.Identity.GetUserId());
                invoice.User = user;

                db.Invoices.Add(invoice);
            }

            // Redirect somewhere meaningful (probably to somewhere showing the results of your processing):
            return RedirectToAction("Index");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Description,Amount,UnitPrice,NettoPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Description,Amount,UnitPrice,NettoPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
