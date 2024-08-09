using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        // Hello World

        public ViewResult Index()
        {
        //    string[] st = { "india", "canda", "japan", "russia" };
        //    ViewData["s"] = st;
        //    ViewBag.str = st;

        //    string a = "Monkey D luffy";
        //    ViewData["k1"] = a;
        //    ViewData["k2"] = "Roronoa Zoro";

        //    ViewData["a"] = 4;
        //    ViewData["b"] = 5;

        //    ViewBag.x = "Vinsmoke Sanji";

        //    ViewBag.i = 34;
        //    ViewBag.j = 46;

            TempData["k"] = "Hello Welcome to my page";
            TempData.Keep("k");
            return View();
        }


        // Doesn't print anything
        public ViewResult Display()
        {
            return View();
        }

        // showdate => prints current date + bg as blue

        [ActionName("ViewDate")]
        public ViewResult ShowDate()
        {
            return View("Showdate");
        }


        //[ActionName("ind")]
        //[NonAction] // now india method cannot be called from browser
        public string india()
        {
            return "welcome to india page";
        }


        [HttpGet]
        public ViewResult Addnums()
        {
            return View();
        }
        // read the value of textbox

        [HttpPost]
        public ViewResult Addnums(string txt1, string txt2)
        {
            int res = int.Parse(txt1) + int.Parse(txt2);

            ViewData["v"] = res;
            return View();
        }

        [HttpGet]
        public ViewResult Printntimes()
        {
            return View();

        }
        [HttpPost]
        public ViewResult Printntimes(string txt1, string txt2)
        {
            ViewData["v"] = txt1;
            ViewData["w"] = txt2;
            return View();
        }

    }
}
