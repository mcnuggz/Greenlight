using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Greenlight.Models;
using System.IO;
using System.Web.Hosting;
using System.Net.Mime;

namespace Greenlight.Controllers
{
    
    public class GamesController : Controller
    {
        private GreenlightContext db = new GreenlightContext();

        [AllowAnonymous]
        public ActionResult CustomerIndex()
        {
            return View(db.Games.ToList());
        }

        [Authorize(Roles = "Developer")]
        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }
        [AllowAnonymous]
        // GET: Games/Details/5
        public ActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Developer")]
        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Genre,Description,Price")] Game game, HttpPostedFileBase file, HttpPostedFileBase demoFile, HttpPostedFileBase gameFile)
        {
            if (ModelState.IsValid)
            {
                string picture = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), picture);
                file.SaveAs(path);

                string demo = Path.GetFileName(demoFile.FileName);
                string demoPath = Path.Combine(Server.MapPath("~/DemoFile/"), demo);
                demoFile.SaveAs(demoPath);

                string fullGame = Path.GetFileName(gameFile.FileName);
                string gamePath = Path.Combine(Server.MapPath("~/GameFile"), fullGame);
                gameFile.SaveAs(gamePath);

                game.ImagePath = file.FileName;
                game.Demo = demoFile.FileName;
                game.FullGame = gameFile.FileName;

                ViewBag.Message = "Success";
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Genre,Description,Price")] Game game, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string picture = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), picture);
                file.SaveAs(path);
                game.ImagePath = file.FileName;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
        [Authorize(Roles = "Developer")]
        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Developer")]
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
