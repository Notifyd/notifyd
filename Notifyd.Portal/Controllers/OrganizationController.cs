using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notifyd.Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Notifyd.Portal.Controllers
{
    public class OrganizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> manager;

        public OrganizationController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: /Organization/
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(db.Organizations.ToList().Where(org => org.Owner.UserName == currentUser.UserName));
        }

        // GET: /ToDo/All
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> All()
        {
            return View(await db.Organizations.ToListAsync());
        }

        // GET: /ToDo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization todo = await db.Organizations.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            //if (todo.Owner.Id != currentUser.Id)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            //}
            return View(todo);
        }

        // GET: /Organization/Create
        public ActionResult Create()

        {
            return View();
        }

        // POST: /ToDo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,URL")] Organization org)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                org.Owner = currentUser;
                db.Organizations.Add(org);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(org);
        }

        // GET: /ToDo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization todo = await db.Organizations.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            if (todo.Owner.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(todo);
        }

        // POST: /ToDo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,URL")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organization);
        }

        // GET: /ToDo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization todo = await db.Organizations.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            if (todo.Owner.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(todo);
        }

        // POST: /ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Organization todo = await db.Organizations.FindAsync(id);
            db.Organizations.Remove(todo);
            await db.SaveChangesAsync();
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
