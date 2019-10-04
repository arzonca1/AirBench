using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirBench.Data;
using System.Threading.Tasks;

namespace AirBench.Controllers
{
    [Authorize]
    public class BenchesController : Controller
    {
        
        IBenchRepository _repository = new BenchRepository();
        IUserRepository iur = new UserRepository();
        // GET: Benches
        [AllowAnonymous]
        async public Task<ActionResult> Index()
        {
            List<Bench> benches = await _repository.GetBenchListAsync();
            return View(benches);
        }

        // GET: Benches/Details/5
        [AllowAnonymous]
        async public Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bench bench = await _repository.GetBenchSingleAsync(id.Value);
            if (bench == null)
            {
                return HttpNotFound();
            }
            return View(bench);
        }

        // GET: Benches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Benches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Create([Bind(Include = "Id,Name,Seats,Longitude,Latitude,Description")] Bench bench)
        {
            if (ModelState.IsValid)
            {
                User user = await iur.GetUser(User.Identity.Name);
                await _repository.AddBenchAsync(0, bench.Seats, bench.Longitude, bench.Latitude, bench.Name, user.Id, bench.Description);
                return RedirectToAction("Index");
            }

            return View(bench);
        }

        // GET: Benches/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bench bench = db.Benches.Find(id);
        //    if (bench == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bench);
        //}

        // POST: Benches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Longitude,Latitude")] Bench bench)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bench).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bench);
        //}

        // GET: Benches/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bench bench = db.Benches.Find(id);
        //    if (bench == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bench);
        //}

        //// POST: Benches/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Bench bench = db.Benches.Find(id);
        //    db.Benches.Remove(bench);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
