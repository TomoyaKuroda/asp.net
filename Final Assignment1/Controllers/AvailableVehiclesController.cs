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
    public class AvailableVehiclesController : Controller
    {
        private OurConnection db = new OurConnection();

        // GET: AvailableVehicles
        public ActionResult Index()
        {
            var vehicle = db.Vehicle.Include(v => v.Make).Include(v => v.Model).Include(v => v.Model.VehicleType);
            vehicle = vehicle.OrderByDescending(v => v.Price);
            vehicle = vehicle.Where(v => v.SoldDate == null);
            return View(vehicle.ToList());
        }

        // GET: AvailableVehicles/Details/5
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

        // GET: AvailableVehicles/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(db.Make, "MakeId", "Name");
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "ModelId");
            return View();
        }

        // POST: AvailableVehicles/Create
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
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "ModelId", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: AvailableVehicles/Edit/5
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
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "ModelId", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: AvailableVehicles/Edit/5
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
            ViewBag.ModelId = new SelectList(db.Model, "ModelId", "ModelId", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: AvailableVehicles/Delete/5
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

        // POST: AvailableVehicles/Delete/5
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
