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
            if (string.IsNullOrWhiteSpace(bench.Name))
            {
                ModelState.AddModelError("", "Name can't be empty");
            }
            if (bench.Seats < 1)
            {
                ModelState.AddModelError("", "Seats has to be at least 1");
            }
            if (bench.Longitude > -180 || bench.Longitude > 180)
            {
                ModelState.AddModelError("", "Longitude has to be between -180 and 180");
            }
            if (bench.Latitude < -90 || bench.Latitude > 90)
            {
                ModelState.AddModelError("", "Latitude has to be between -90 and 90");
            }
            if (string.IsNullOrWhiteSpace(bench.Description))
            {
                ModelState.AddModelError("", "Description can't be empty");
            }

            if (ModelState.IsValid)
            {
                User user = await iur.GetUser(User.Identity.Name);
                await _repository.AddBenchAsync(0, bench.Seats, bench.Longitude, bench.Latitude, bench.Name, user.Id, bench.Description);
                return RedirectToAction("Index");
            }

            return View(bench);
        }
        async public Task<ActionResult> Comment(int id)
        {
            Comment comment = new Comment(); 
            return View("Comment", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Comment(int id, [Bind(Include ="Id,Text,Rating,UserId")] Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Text))
            {
                ModelState.AddModelError("", "Text can't be empty");
            }
            if(comment.Rating < 1 || comment.Rating > 5)
            {
                ModelState.AddModelError("", "Rating has to be between 1 and 5 stars");
            }
            if (ModelState.IsValid)
            {
                User user = await iur.GetUser(User.Identity.Name);
                await _repository.AddCommentAsync(id, comment.Text, comment.Rating, user.Id);
                return RedirectToAction("Details", new {id=id});
            }
            
            return View("Comment");
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
