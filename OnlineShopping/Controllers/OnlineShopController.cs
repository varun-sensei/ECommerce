using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Models;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace OnlineShopping.Controllers
{
    public class OnlineShopController : Controller
    {
        // IActionResult returns view,string, json, javascript, etc....

        // ViewResult only returns view

        //home,login,register,viewproduct,buy,feedback,search,error

        onlineshopdbContext dc = new onlineshopdbContext();

        public List<Product> li = new List<Product>();


        public IActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string t1,string t2)
        {
            var res = (from t in dc.Registers
                      where t.Uname == t1 && t.Password == t2
                      select t).Count();
            if(res > 0)
            {
                HttpContext.Session.SetString("uid", t1);
                return RedirectToAction("ViewProduct");
            }
            else
            {
                ViewData["msg"] = "Invalid Credentials..... Please try again!!";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uid");
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register r)
        {
            if (ModelState.IsValid)
            {
                dc.Registers.Add(r);
            }
                int i = dc.SaveChanges();
                if (i > 0)
                {
                    ViewData["msg"] = "New User added successfully";
                }
                else
                {
                    ViewData["msg"] = "Could not Create User try again!";
                }
            
            return View();
        }

        [HttpGet]
        public IActionResult ViewProduct()
        {

        li = (from t in dc.Products
              select t).ToList();
            return View(li);
        }

        [HttpGet]
        public IActionResult Buy(string pid)
        {
            if (HttpContext.Session.GetString("uid") == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("proid","pid");
            TempData["k"] = pid;
            TempData.Keep(pid);
            var r = (from t in dc.Products
                     where t.Pid == pid
                     select t).ToList();
            return View(r);
        }
        [HttpPost]
        public IActionResult Buy(int qty)
        {
            var uid = HttpContext.Session.GetString("uid");

            Userorder u = new Userorder();
            Feedbacktbl f = new Feedbacktbl();

            f.Username = uid;
            f.Pid = Request.Query["pid"];
            f.Fstatus = false;

            dc.Feedbacktbls.Add(f);

            u.Username = uid;
            u.Transdate = DateOnly.FromDateTime(DateTime.Now);
            u.Pid = Request.Query["pid"];
            u.Qty = qty;

            dc.Add(u);
            int i = dc.SaveChanges();
            if(i > 0)
            {
                ViewData["msg"] = "Order placed successfully!!";
            }
            else
            {
                ViewData["msg"] = "Some error occured";
            }
            return View();
        }

        [HttpGet]
   
        public IActionResult Search(string txtsearch)
        {
            var r = (from t in dc.Products
                      where t.Pname.Contains(txtsearch)
                      select t).ToList();

            var res = (from t in dc.Products
                       where t.Pname.Contains(txtsearch)
                       select t).Count();
            if (res > 0)
            {
                ViewData["msg"] = $"{res} Products found";
            }
            else
            {
                ViewData["msg"] = "Product not found..... Please try again!!";
            }

            return View(r);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            if (HttpContext.Session.GetString("uid") == null)
            {
                return RedirectToAction("Login");
            }
            
                var items = new List<SelectListItem>();


            var res = (from t in dc.Feedbacktbls
                       where t.Username == HttpContext.Session.GetString("uid") && t.Fstatus == false
                       select t).ToList();

                foreach (var item in res)
                {
                    items.Add(new SelectListItem { Value = item.Pid });
                }

                ViewBag.DropDownData = items;

                return View();
            
        }

        [HttpGet]
        public PartialViewResult TestPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult TestPartial(Feedbacktbl f)
        {

            var res = (from t in dc.Feedbacktbls
                       where t.Fstatus==false && t.Pid == f.Pid && t.Username == HttpContext.Session.GetString("uid") 
                       select t).First();

            res.Fstatus = true;
            res.Usermessage = f.Usermessage;
            res.Ratings = f.Ratings;
                       
                dc.Feedbacktbls.Update(res);
                int i = dc.SaveChanges();
                if (i > 0)
                {
                    ViewData["msg"] = "Thank you for your valuable feedback!";
                }
                else
                {
                    ViewData["msg"] = "Please give feedback!";
                }
                return View();
        }


        public IActionResult Testing()
        {
            throw new DivideByZeroException();
            return View();
        }
    }
}
