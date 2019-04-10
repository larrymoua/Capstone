using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoloCapstone.Controllers
{
    public class GoalsAndActualController : Controller
    {
        ApplicationDbContext db;
        public GoalsAndActualController()
        {
            db = new ApplicationDbContext();
        }
        // GET: GoalsAndActual
        public ActionResult Index()
        {
            return View();
        }

        // GET: GoalsAndActual/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GoalsAndActual/Create
        public ActionResult Create()
        {
            Goal newGoal = new Goal();
            return View(newGoal);
        }

        // POST: GoalsAndActual/Create
        [HttpPost]
        public ActionResult Create(Goal newGoal)
        {
            try
            {
                Actual actual = new Actual();
                newGoal.Id = actual.GoalFK;
                newGoal = actual.Goal;
                db.Goals.Add(newGoal);
                db.Actuals.Add(actual);
                db.SaveChanges();

                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View("Index", "Order");
            }
        }

        // GET: GoalsAndActual/Edit/5
        public ActionResult Edit(int id)
        {
            var findGoal = db.Goals.Find(id);
            var findActual = db.Actuals.Where(a => a.GoalFK == findGoal.Id).Single();
            GoalAndActualModel goalAndActualModel = new GoalAndActualModel()
            {
                goal = new Goal(),
                actual = new Actual()
            };
            goalAndActualModel.goal = findGoal;
            goalAndActualModel.actual = findActual;
            return View(goalAndActualModel);
        }

        // POST: GoalsAndActual/Edit/5
        [HttpPost]
        public ActionResult Edit(GoalAndActualModel goalAndActualModel)
        {
            var findGoal = db.Goals.Find(goalAndActualModel.goal.Id);
            var findActual = db.Actuals.Where(a => a.GoalFK == goalAndActualModel.goal.Id).SingleOrDefault();
            try
            {
                findGoal.Monday = goalAndActualModel.goal.Monday;
                findGoal.Tuesday = goalAndActualModel.goal.Tuesday;
                findGoal.Wednesday = goalAndActualModel.goal.Wednesday;
                findGoal.Thursday = goalAndActualModel.goal.Thursday;
                findGoal.Friday = goalAndActualModel.goal.Friday;
                findGoal.StartDate = goalAndActualModel.goal.StartDate;
                findGoal.EndDate = goalAndActualModel.goal.EndDate;

                findActual.Monday = goalAndActualModel.actual.Monday;
                findActual.Tuesday = goalAndActualModel.actual.Tuesday;
                findActual.Wednesday = goalAndActualModel.actual.Wednesday;
                findActual.Thursday = goalAndActualModel.actual.Thursday;
                findActual.Friday = goalAndActualModel.actual.Friday;

                db.SaveChanges();

                return RedirectToAction("Index", "ProductionManager");
            }
            catch
            {
                return View();
            }
        }

        // GET: GoalsAndActual/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GoalsAndActual/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
