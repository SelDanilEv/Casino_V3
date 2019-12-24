using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino_V3.Models;
using Microsoft.AspNetCore.Http;

namespace Casino_V3.Controllers
{
    public class AuthController : Controller
    {
        private static AuthIndexViewModel obj;

        public IActionResult Index()
        {
            if (obj == null)
                obj = new AuthIndexViewModel();
            return View(obj);
        }

        private int returnedId;

        public IActionResult Log_in (string username,string password)
        {
            returnedId = DataBase.Authorization(username, password);
            switch (returnedId)
            {
                case -2:
                    obj.Message = "Invalid Login";
                    break;
                case -1:
                    obj.Message = "Invalid Password or Login";
                    break;
                default:
                    obj.Message = "";
                    BindClass.userId = returnedId;
                    DataBase.ActivUsers.ToArray()[returnedId].CorrectTransition = true;
                    //return  RedirectToPage("Account");
                    return RedirectToActionPermanent("ShowAccount", "Account");
                    //return RedirectPermanent("/Account/ShowAccount");
            }
            return RedirectPermanent("Index");
        }

        public async void Serialize()
        {
            await Task.Run(() => DataBase.Serialize("UsersFile", DataBase.ActivUsers));
        }

        public IActionResult Log_on(string username, string password)
        {
            User user = new User(username, password, 1000);
            bool flag = DataBase.AddUser(user);
            obj = new AuthIndexViewModel();
            if (flag)
            {
                obj.Message = "Account was created";
                Serialize();
            }
            else obj.Message = "This name was occupied";
            return RedirectToAction("Index", "Auth");
        }
    }
}
