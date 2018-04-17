using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TempDataDemo.Data;
using TempDataDemo.Models;

namespace TempDataDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePageViewModel vm = new HomePageViewModel();
            PersonDb db = new PersonDb(Properties.Settings.Default.ConStr);
            vm.People = db.GetPeople();
            if (TempData["Rosenberg"] != null)
            {
                vm.Message = (string) TempData["Rosenberg"];
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            PersonDb db = new PersonDb(Properties.Settings.Default.ConStr);
            db.AddPerson(person);
            TempData["Rosenberg"] = $"Person added successfully, new Id: {person.Id}";
            return Redirect("/");
        }
    }


}