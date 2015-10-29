using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class DinnersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public DinnersController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        }

        // GET: Dinners
        public ActionResult Index(string searchString)
        {
            var dinners = from m in db.Dinners select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                dinners = dinners.Where(s => s.name.Contains(searchString));
            }
            return View(dinners.ToList());

        }

        // GET: Dinners
        //public ActionResult Index(bool? join)
        //{
        //    ApplicationUser u = manager.FindById(User.Identity.GetUserId());
        //    ViewData.Add("UserID", u.Id);

        //    return View(db.Dinners.ToList());
        //}

        [Authorize]
        public ActionResult Join(int id)
        {
            Dinner d = db.Dinners.Find(id);
            ApplicationUser u = manager.FindById(User.Identity.GetUserId());
            //Participant p = new Participant();
            d.NumberAttending = d.NumberAttending + 1;
            //p.ParticipantID = db.Participants.Count() + 1;
            //p.dinner = d;
            //p.User = u;
            d.Participants.Add(u);
            //db.Participants.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Unjoin(int id)
        {
            Dinner d = db.Dinners.Find(id);
            ApplicationUser u = manager.FindById(User.Identity.GetUserId());
            //Participant p = new Participant();
            d.NumberAttending = d.NumberAttending - 1;
            //p.ParticipantID = db.Participants.Count() + 1;
            //p.dinner = d;
            //p.User = u;
            d.Participants.Remove(u);
            //db.Participants.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // GET: Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dinnerID,name,description,dinnerMonth,dinnerDay,dinnerYear,NumberOfCourses,NumberAttending")] Dinner dinner)
        {
            dinner.User = manager.FindById(User.Identity.GetUserId());
            dinner.NumberAttending = 0;
            if (ModelState.IsValid)
            {
                db.Dinners.Add(dinner);
                db.SaveChanges();
                return RedirectToAction("CreateDinnerCourse");
            }

            return View(dinner);
        }

        public ActionResult CreateDinnerCourse()
        {
            ViewBag.DinnerID = new SelectList(db.Dinners, "DinnerID", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDinnerCourse([Bind(Include = "CourseID,UserID,DinnerID,CourseDescription,CourseTime,Address")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                Dinner dinner = db.Dinners.Find(course.dinnerID);
                int courseCount = db.Courses.Where(i => i.dinnerID == course.dinnerID).Count();
                if (courseCount == dinner.NumberOfCourses)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("CreateDinnerCourse");
                }
            }

            ViewBag.DinnerID = new SelectList(db.Dinners, "DinnerID", "Name", course.dinnerID);
            return View(course);
        }

        // GET: Dinners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // POST: Dinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dinnerID,name,description,dinnerMonth,dinnerDay,dinnerYear,NumberOfCourses,NumberAttending")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dinner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // POST: Dinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinner dinner = db.Dinners.Find(id);
            dinner.Courses.Clear();
            db.Dinners.Remove(dinner);
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

        // GET: Dinners/Details/5
        public ActionResult AttendingParticipants(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View("AttendingParticipants");
        }
    }
}
