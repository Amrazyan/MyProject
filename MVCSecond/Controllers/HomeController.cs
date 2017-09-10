using MVCSecond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;

namespace MVCSecond.Views.Home
{
    public class HomeController : Controller
    {
        /// <summary>
        ///             LOGIN FORM
        /// </summary>
        ///
        /// <returns></returns>

        public ActionResult Index()
        {
            
            return View();
        }
       
        [HttpPost]
        public ActionResult Index(string Email,string Password)
        {

            dasDBEntities1 db1 = new dasDBEntities1();
           
           

            if (db1.Users.ToList().Exists(u => u.Email == Email && u.Password == Password))
            {
                var usr = db1.Users.Single(u => u.Email == Email && u.Password == Password);

             


                List<AllModel> order = (from or in db1.Orders
                                        
                                            join av in db1.Avand
                                            on or.avand_id equals av.Id
                                        
                                            join b in db1.Banks
                                            on or.bank_id equals b.Id
                                            join use in db1.Users
                                            on or.user_id equals use.Id
                                            where use.Id == usr.Id
                                             
                                            select new AllModel {
                                                Name = use.Name,
                                                Surname = use.Surname,
                                                AvandName = av.AvandName,
                                                Percent = av.Percent,
                                                Price = or.Price,
                                                BankName = b.BankName,
                                                order_date = or.order_date
                                            }).ToList();
                if (!order.Any())
                {
                    ViewBag.Err = "You haven't any orders yet ";
                }
                var br = (from bank in db1.Banks
                          select  new AllModel { Bank = bank.BankName, Info = bank.Information }).ToList();

                order.AddRange(br);

                var tst = new intermediate();
                tst.Enumallmodel = order;
                tst.Normallmodel = new AllModel();//               

                ViewBag.bank = new SelectList(db1.Banks, "Id", "BankName");
                ViewBag.avandtype = new SelectList(db1.Avand, "Id", "AvandName");

                Session["Userid"]=usr.Id;     //Vor Id-na stacel
                Session["Username"] = usr.Name.ToString();
                Session["Surname"] = usr.Surname.ToString();
               
                return View("Account", tst);
            }
            else
            {
                ModelState.AddModelError("Password", "Wrong login or password");
            }
            return View();
        }
      
      [HttpPost]
      
        public ActionResult Updte(string prce,string bank,string avandtype)
        {
            dasDBEntities1 db1 = new dasDBEntities1();

            int bnkid = Convert.ToInt32(bank);
            int avndtype = Convert.ToInt32(avandtype);
           

            Orders ord = new Orders();
            ord.user_id = Convert.ToInt32(Session["Userid"]);
            ord.avand_id = avndtype;
            ord.bank_id = bnkid;
            ord.order_date = DateTime.Now;
            
            ord.Price = Convert.ToInt32(prce);
          
         

            db1.Orders.Add(ord);
            db1.SaveChanges();

            int usr = Convert.ToInt32(Session["Userid"]);

            IEnumerable<AllModel> order = (from or in db1.Orders

                                           join av in db1.Avand
                                           on or.avand_id equals av.Id

                                           join b in db1.Banks
                                           on or.bank_id equals b.Id
                                           join use in db1.Users
                                           on or.user_id equals use.Id
                                           where use.Id == usr
                                           select new AllModel
                                           {
                                               Name = use.Name,
                                               Surname = use.Surname,
                                               AvandName = av.AvandName,
                                               Percent = av.Percent,
                                               Price = or.Price,
                                               BankName = b.BankName,
                                               order_date = or.order_date
                                           }).ToList();



            return PartialView("_Order",order);
        }

        /// <summary>
        ///         REGISTER FORM
        /// </summary>
        /// <returns></returns>
        /// 
          public ActionResult Register()
        {
            Session.Clear();
            return View();
        }
       
        [HttpPost]
        public ActionResult Register(Users model,string PasswordConfirm)
        {
           

            if (PasswordConfirm != model.Password)
            {
                ModelState.AddModelError("Password", "Passwords do not match");
            }
            if (ModelState.IsValid)
            {
                dasDBEntities1 db = new dasDBEntities1();
               

                if (db.Users.ToList().Exists(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Тhis email is already in use ");
                }
                else
                {
                    //  db.User.Add(new User { Name = model.Name, Surname = model.Surname, Email = model.Email, Password = model.Password });
                    db.Users.Add(model);
                    db.SaveChanges();
                    Session["suc"]= "Registration success";
                    return RedirectToAction("Index","Home");
                }
            }
            
                return View();           
        }
       
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}