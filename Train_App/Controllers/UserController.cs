using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Train_App.DataAccess;
using Train_App.Models;

namespace Shopping_App.Controllers
{
    public class UserController : Controller
    {
        IUser User;
        public UserController(IUser User)
        {
            this.User = User;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult MyDetails()
        {
            return View(User.FetchMyDetails());
        }

        public IActionResult UpdateWallet()
        {
            return View(User.FetchMyDetails());
        }

        public IActionResult ShowTrains()
        {
            List<TrainDetails> trains = User.FetchAvailableTrainDetails();

            ViewBag.FromList = trains.Select(t => t.FromLoc).Distinct().ToList();
            ViewBag.ToList = trains.Select(t => t.ToLoc).Distinct().ToList();

            return View(trains);
        }

        public IActionResult BookTrain(int id)
        {
            return View(User.FetchAvailableTrainDetails().FirstOrDefault(t => t.Id == id));
        }

        public IActionResult ShowBookedTrain()
        {
            return View(User.FetchBookedTrainDetails());
        }

        public IActionResult ShowCancelledTrains()
        {
            return View(User.FetchCancelledTrainDetails());
        }

        public IActionResult CancelTrains()
        {
            return View(User.FetchBookedTrainDetails());
        }
    }
}