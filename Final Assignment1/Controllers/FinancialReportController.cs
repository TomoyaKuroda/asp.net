using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Assignment1.Models;

namespace Final_Assignment1.Controllers
{
    public class FinancialReportController : Controller
    {
        private OurConnection db = new OurConnection();

        // GET: FinancialReport
        public ActionResult Index()
        {
            var vehicle = db.Vehicle.Include(v => v.Make).Include(v => v.Model).Include(v => v.Model.VehicleType);
            var vehiclesold = vehicle.Where(v => v.SoldDate != null);
            var income = vehiclesold.Sum(v =>v.Price);
            ViewBag.TotalVehiclesSold = vehiclesold.Count();
            ViewBag.income = income;
            var vehicleavailable = vehicle.Where(v => v.SoldDate == null);
            ViewBag.TotalVehiclesAvailable = vehicleavailable.Count();
            return View(vehicle.ToList());
        }

        // GET: FinancialReport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: FinancialReport/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(db.Make, "MakeId", "Name");
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "Colour");
            return View();
        }

        // POST: FinancialReport/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,MakeId,ModelId,Year,Price,SoldDate")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicle.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakeId = new SelectList(db.Make, "MakeId", "Name", vehicle.MakeId);
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "Colour", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: FinancialReport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(db.Make, "MakeId", "Name", vehicle.MakeId);
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "Colour", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: FinancialReport/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleId,MakeId,ModelId,Year,Price,SoldDate")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(db.Make, "MakeId", "Name", vehicle.MakeId);
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "Colour", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: FinancialReport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: FinancialReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicle.Find(id);
            db.Vehicle.Remove(vehicle);
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
