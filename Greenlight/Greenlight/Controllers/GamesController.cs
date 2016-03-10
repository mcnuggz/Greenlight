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

namespace Greenlight.Controllers
{
    [Authorize(Roles ="Developer")]
    public class GamesController : Controller
    {
        private GreenlightContext db = new GreenlightContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

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

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

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


                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

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
