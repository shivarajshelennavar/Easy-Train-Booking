using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Train_App.DataAccess;
using Train_App.Models;

namespace Train_App.Controllers
{
    public class ServiceController : Controller
    {
        IAdmin Admin;
        IUser User;
        public ServiceController(IAdmin Admin, IUser User)
        {
            this.Admin = Admin;
            this.User = User;
        }

        //ADMIN PART
        [HttpGet]
        public bool loginAdmin(int id, string password)
        {
            return Admin.LoginAdmin(id, password);
        }

        [HttpPost]
        public bool updateTrain([FromBody]TrainDetails train)
        {
            return Admin.UpdateTrain(train);
        }

        [HttpPost]
        public bool addTrain([FromBody]TrainDetails train)
        {
            return Admin.AddTrain(train);
        }

        [HttpGet]
        public bool deleteTrain(int id)
        {
            return Admin.DeleteTrain(id);
        }


        //USER PART
        [HttpPost]
        public bool registerUser([FromBody]UserDetails user)
        {
            return User.RegisterUser(user);
        }

        [HttpGet]
        public bool loginUser(long phone, string pass)
        {
            return User.LoginUser(phone, pass);
        }

        [HttpGet]
        public bool updateWallet(decimal amt)
        {
            return User.UpdateWallet(amt);
        }

        [HttpPost]
        public bool bookTicket(int Tnum, string seatType, int count, DateTime date)
        {
            return User.BookTicket(Tnum, seatType, count, date);
        }

        [HttpGet]
        public bool cancelTrain(int id)
        {
            return User.CancelTicket(id);
        }

    }
}