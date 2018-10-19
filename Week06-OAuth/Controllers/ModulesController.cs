using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week06_OAuth.Models;
using Microsoft.AspNet.Identity;

namespace Week06_OAuth.Controllers
{
    public class ModulesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Modules
        public ActionResult Index()
        {            
            ViewData["UserId"] = User.Identity.GetUserId();
            return View(db.Modules.ToList());
        }

        public ActionResult ModulesByUser()
        {
            string StudentID = User.Identity.GetUserId();
            AspNetUser Student = db.AspNetUsers.Find(StudentID);
            List<Module> Modules = db.Modules.Where(m => m.Student.Id == StudentID).ToList();

            return View(Modules);
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            List<AspNetUser> Students = db.AspNetUsers.ToList();
            AspNetUser Student = Students.Where(s => s.Modules.Contains(module)).FirstOrDefault();

            ViewData["Student"] = Student != null ? Student.UserName : null;
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AssignStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewData["Students"] = db.AspNetUsers.ToList();
            return View(module);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AssignStudent(int id)
        {
            id = int.Parse(Request.Form["id"]);
            string studentId = Request.Form["student"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            AspNetUser student = db.AspNetUsers.Find(studentId);
            if (student == null)
            {
                return HttpNotFound();
            }
            module.Student = student;
            student.Modules.Add(module);
            db.SaveChanges();

            ViewData["Students"] = db.AspNetUsers.ToList();
            return View("Details", module);
        }


        // GET: Modules/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        [Authorize]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleId,MacAddress")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        [Authorize]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleId,MacAddress")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
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
