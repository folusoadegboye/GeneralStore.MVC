using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customerList = _db.Customers.ToList();
            List<Customer> customerorderlist = customerList.OrderBy(c => c.FullName).ToList();

            return View(customerorderlist);
        }

        //GET:Create
        public ActionResult Create()
        {
            return View();
        }
        //POST:Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer  customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        //GET:Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if(customer is null)
            {
                return HttpNotFound("Customer nit found");
            }
            return View(customer);
        }
        //GET:Edit
        public ActionResult Edit(int? id)
        {
            return Details(id);
        }
        //POST:Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(customer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
           
        }
        //GET:Delete
        public ActionResult Delete(int? id)
        {
            return Details(id);
        }
        //POST:Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}