using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity_AlekK.DAL;
using ContosoUniversity_AlekK.Models;
using PagedList;

namespace ContosoUniversity_AlekK.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();
        private IStudentRepository studentRepository;
        public StudentController()
        {
            this.studentRepository = new StudentRepository(new SchoolContext());
        }

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in studentRepository.GetStudents() //db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) ||
                                            s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Student student = studentRepository.GetStudentByID(id); //db.Students.Find(id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Students.Add(student);
                    //db.SaveChanges();
                    studentRepository.InsertStudent(student);
                    studentRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.DataException /*dex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Student student = db.Students.Find(id);
            int idInt = Convert.ToInt32(id);
            Student student = studentRepository.GetStudentByID(idInt);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var studentToUpdate = db.Students.Find(id);
            int idInt = Convert.ToInt32(id);
            var studentToUpdate = studentRepository.GetStudentByID(idInt);
            if (TryUpdateModel(studentToUpdate, "", new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    //db.SaveChanges();
                    studentRepository.Save();

                    return RedirectToAction("Index");
                }
                catch (System.Data.DataException /*dex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again and then contact system admin!");
                }
                //if (ModelState.IsValid)
                //{
                //    db.Entry(student).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int? id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Грешка при бришење, контактирајте техничко лице";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Student student = db.Students.Find(id);
            int idInt = Convert.ToInt32(id);
            Student student = studentRepository.GetStudentByID(idInt);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]/*, ActionName("Delete")*/
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //Student student = db.Students.Find(id);
                //db.Students.Remove(student);

                //Student student = new Student { ID = id };
                //db.Entry(student).State = EntityState.Deleted;
                //db.SaveChanges();
                studentRepository.DeleteStudent(id);
                studentRepository.Save();
            }
            catch (System.Data.DataException)
            {
                RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
