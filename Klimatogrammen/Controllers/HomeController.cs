using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            HttpContext.Session.RemoveAll();
            return View();
        }

    }
}