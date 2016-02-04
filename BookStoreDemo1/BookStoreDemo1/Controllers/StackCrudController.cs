using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreDemo1.Models;

namespace BookStoreDemo1.Controllers
{
    public class StackCrudController : Controller
    {
        private BookStoreDbContext db = new BookStoreDbContext();

        // GET: /StackCrud/
        public ActionResult Index()
        {
            return View(db.Stacks.ToList());
        }

        // GET: /StackCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stack stack = db.Stacks.Find(id);
            if (stack == null)
            {
                return HttpNotFound();
            }
            return View(stack);
        }

        // GET: /StackCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StackCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StackId,Location")] Stack stack)
        {
            if (ModelState.IsValid)
            {
                db.Stacks.Add(stack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stack);
        }

        // GET: /StackCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stack stack = db.Stacks.Find(id);
            if (stack == null)
            {
                return HttpNotFound();
            }
            return View(stack);
        }

        // POST: /StackCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StackId,Location")] Stack stack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stack);
        }

        // GET: /StackCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stack stack = db.Stacks.Find(id);
            if (stack == null)
            {
                return HttpNotFound();
            }
            return View(stack);
        }

        // POST: /StackCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stack stack = db.Stacks.Find(id);
            db.Stacks.Remove(stack);
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
