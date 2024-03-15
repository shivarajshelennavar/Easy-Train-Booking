using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Train_App.DataAccess;

namespace Train_App.Controllers
{
    public class AdminController : Controller
    {
        IAdmin Admin;
        public AdminController(IAdmin Admin)
        {
            this.Admin = Admin;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ShowTrains()
        {
            return View(Admin.FetchAllTrains());
        }

        public IActionResult ShowUsers()
        {
            return View(Admin.FetchUserDetails());
        }

        public IActionResult UpdateTrains()
        {
            return View(Admin.FetchAllTrains());
        }

        public IActionResult Update(int id)
        {
            return View(Admin.FetchAllTrains().FirstOrDefault(t => t.Id == id));
        }

        public IActionResult AddTrain()
        {
            return View();
        }
        public IActionResult DeleteTrain(int id)
        {
            return View(Admin.FetchAllTrains().FirstOrDefault(t => t.Id == id));
        }
    }
}