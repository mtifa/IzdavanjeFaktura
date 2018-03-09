using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Mvc;
using IssuingInvoices.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.Composition;
using IssuingInvoices.MEFExtensions;

namespace IssuingInvoices.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private InvoicesModel db = new InvoicesModel();
        private ApplicationUserManager userManager;
        [Import(typeof(IVatSetUp))] private IVatSetUp vatCalculator;

        public InvoicesController()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            MefContentLoader.Compose(this);
        }
        
        // GET: Invoices
        public ActionResult Index()
        {
            return View(db.Invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,DeliverDate,Vat,ClientName")] Invoice invoice) 
        {
            if (!ModelState.IsValid) return View(invoice);

            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            invoice.User = user; 
            invoice.DateCreated = DateTime.Now;
            db.Invoices.Add(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,DateCreated,DeliverDate,Vat,TotalPrice,ClientName")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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

        #region Products
        public IEnumerable<Product> GetProducts(int? invoiceId)
        {
            var products = db.Products.Where(i => i.Invoice.InvoiceId == invoiceId);

            return products;
        }
        public Product Get(int? productId)
        {
            var item = db.Products.FirstOrDefault(i => i.ProductId == productId);
            return item;
        }
        // GET: Invoices/Products/5
        public ActionResult Products(int? id)
        {
            var products = GetProducts(id); 

            if (products == null)
            {
                return HttpNotFound();
            }

            Invoice invoice = db.Invoices.FirstOrDefault(i => i.InvoiceId == id);
            ViewData.Add("productId", id);
            ViewData.Add("invoiceId", invoice.InvoiceId);

            return View(products);
        }

        // GET: Invoices/AddProduct/5
        public ActionResult AddProduct(int? id)
        {
            var user = User.Identity.GetUserId();
            Invoice invoice = db.Invoices.FirstOrDefault(i => i.InvoiceId == id);
            ViewData.Add("invoiceId", id);
            ViewData.Add("productId", invoice.InvoiceId);

            return View();
        }
        // POST: Invoices/AddProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct([Bind(Include = "Description,Amount,UnitPrice")] Product item, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            var user = User.Identity.GetUserId();
            var invoice = db.Invoices.Include(i => i.Products).FirstOrDefault(i => i.InvoiceId == id);

            item.NettoPrice = item.UnitPrice * item.Amount;
            item.Invoice = invoice;
            invoice.Netto += item.NettoPrice;
            var totalprice = vatCalculator.CalculateBruttoPrice(invoice.Vat, invoice.Netto);
            invoice.TotalPrice = totalprice;

            db.Entry(invoice).State = EntityState.Modified;

            db.Products.Add(item);
            db.SaveChanges();
            return RedirectToAction("Products", new { id });
        }

        // POST: Invoices/DeleteProduct/7
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int? id, int? invoiceId)
        {
            var item = db.Products.FirstOrDefault(i => i.ProductId == id);
            var invoice = db.Invoices.Include(i => i.Products).FirstOrDefault(i => i.InvoiceId == invoiceId);
            invoice.Netto -= item.NettoPrice;
            var totalprice = vatCalculator.CalculateBruttoPrice(invoice.Vat, invoice.Netto);
            invoice.TotalPrice = totalprice;

            db.Products.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Products", new { id = invoiceId });
        }
        #endregion
    }
}
