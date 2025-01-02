using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAssessmentQuestion2.Models;

namespace MVCAssessmentQuestion2.Controllers
{
    public class MovieController : Controller
    {

        MovieContext db = new MovieContext();
        public ActionResult Index()
        {
            return View(db.movies.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie c)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

       

        public ActionResult Edit(int id)
        {
            var mv = db.movies.Find(id);
            if (mv != null)
            {
                return View(mv);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(Movie c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }      


        public ActionResult YearMovies()
        {
            return View();
        }


        [HttpPost, ActionName("YearMovieResult")]
        public ActionResult YearMovieResult(DateTime DateofRelease)
        {
            int year =DateofRelease.Year;
            var Movies = db.movies.Where(dt => dt.DateofRelease.Year == year);
            return View(Movies);
        }




    }
}