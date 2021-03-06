using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity_AlekK.DAL;
using ContosoUniversity_AlekK.Models;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity_AlekK.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db = new SchoolContext();
        private UnitOfWork UnitOfWork = new UnitOfWork();

        // GET: Course
        public ActionResult Index()
        {
            //var courses = db.Courses.Include(d => d.Department);
            var courses = UnitOfWork.CourseDepository.Get(includeProperties: "Department");
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Course course = db.Courses.Find(id);
            Course course = UnitOfWork.CourseDepository.GetByID(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            //return View();
            PopulateDepartmentsDropDownList();
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = UnitOfWork.DepartmentRepository.Get(
                                            orderBy: q => q.OrderBy(d => d.Name)); 
                                   //from d in db.Departments
                                   //orderby d.Name
                                   //select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Courses.Add(course);
                    UnitOfWork.CourseDepository.Insert(course);
                    //db.SaveChanges();
                    UnitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception /*dex*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }


            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Course course = db.Courses.Find(id);
            Course course = UnitOfWork.CourseDepository.GetByID(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id) //[Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(course).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            //return View(course);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var courseToUpdate = db.Courses.Find(id);
            var courseToUpdate = UnitOfWork.CourseDepository.GetByID(id);
            if (TryUpdateModel(courseToUpdate, "", new string[] { "Title", "Credits", "DepartmentID" }))
            {
                try
                {
                    //db.SaveChanges();
                    UnitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(courseToUpdate);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Course course = db.Courses.Find(id);
            Course course = UnitOfWork.CourseDepository.GetByID(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Course course = db.Courses.Find(id);
            Course course = UnitOfWork.CourseDepository.GetByID(id);
            //db.Courses.Remove(course);
            UnitOfWork.CourseDepository.Delete(course);
            //db.SaveChanges();
            UnitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
