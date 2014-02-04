using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notifyd.Portal.Models;
using Notifyd.Portal.CustomAttributes;

namespace Notifyd.Portal.Controllers
{
    [NotifydAuthAttribute]
    public class AddressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Address/
        public ActionResult Index()
        {
            return View(db.Addresses.ToList());
        }

        // GET: /Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /Address/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AddressId,OrganizationId,Name,Email")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.UpdatedBy = User.Identity.Name;
                address.UpdatedOn = System.DateTime.Now;

                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
        }

        // GET: /Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AddressId,OrganizationId,Name,Email,UpdatedOn,UpdatedBy")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: /Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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

[HttpPost]
        public ActionResult Add(string NewAddress)
{
     try
    {
        Address newaddy = new Address();

        newaddy.Name = NewAddress;
        newaddy.Email = NewAddress;
        newaddy.OrganizationId = 1;

        newaddy.UpdatedBy = User.Identity.Name;
        newaddy.UpdatedOn = System.DateTime.Now;

        db.Addresses.Add(newaddy);
        db.SaveChanges();
     
        return Content("Address Saved!");
    }
    catch (Exception)
    {
        return Content("There was an error... please try again.");
    }
}
    }
}
