using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCSecond.Models;

namespace MVCSecond.Controllers
{
    public class OrdersController : Controller
    {
        private dasDBEntities1 db = new dasDBEntities1();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Avand).Include(o => o.Banks).Include(o => o.Users);
            return View(orders.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Search, string fromm, string to)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<Orders> orders = from or in db.Orders
                                             join av in db.Avand
                                             on or.avand_id equals av.Id
                                             join b in db.Banks
                                             on or.bank_id equals b.Id
                                            join us in db.Users
                                            on or.user_id equals us.Id
                                            
                                             where av.AvandName.Contains(Search) || b.BankName.Contains(Search) || us.Name.Contains(Search)
                                          select or;
                return View(orders.ToList());
            }
            if (!string.IsNullOrEmpty(fromm) && !string.IsNullOrEmpty(to))
            {
                int frome = Convert.ToInt32(fromm);
                int too = Convert.ToInt32(to);
                IEnumerable<Orders> orders = from or in db.Orders
                                             join av in db.Avand
                                             on or.avand_id equals av.Id
                                             join b in db.Banks
                                             on or.bank_id equals b.Id
                                             join us in db.Users
                                             on or.user_id equals us.Id

                                             where or.Price >= frome && or.Price <= too
                                             select or;
                return View(orders.ToList());
            }
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.avand_id = new SelectList(db.Avand, "Id", "AvandName");
            ViewBag.bank_id = new SelectList(db.Banks, "Id", "BankName");
            ViewBag.user_id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,order_date,user_id,avand_id,bank_id,Price")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.avand_id = new SelectList(db.Avand, "Id", "AvandName", orders.avand_id);
            ViewBag.bank_id = new SelectList(db.Banks, "Id", "BankName", orders.bank_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "Name", orders.user_id);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.avand_id = new SelectList(db.Avand, "Id", "AvandName", orders.avand_id);
            ViewBag.bank_id = new SelectList(db.Banks, "Id", "BankName", orders.bank_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "Name", orders.user_id);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,order_date,user_id,avand_id,bank_id,Price")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.avand_id = new SelectList(db.Avand, "Id", "AvandName", orders.avand_id);
            ViewBag.bank_id = new SelectList(db.Banks, "Id", "BankName", orders.bank_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "Name", orders.user_id);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
