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
        private static List<AuthIndexViewModel> obj = new List<AuthIndexViewModel>() { new AuthIndexViewModel() };
        private static int counterObj = 0;
        private static bool standart = true;
        private int returnedId;


        public IActionResult Index()
        {
            return View(obj[counterObj]);
        }

        public IActionResult Log_out()
        {
            counterObj = 0;
            return RedirectToAction("Index", "Auth");
        }

        public IActionResult Log_in(string playername, string password)
        {
            bool toIndex = true;
            AuthIndexViewModel localobj = new AuthIndexViewModel();
            returnedId = DataBase.Authorization(playername, password);
            switch (returnedId)
            {
                case -2:
                    localobj.Message = "Invalid Login";
                    break;
                case -1:
                    localobj.Message = "Invalid Password or Login";
                    break;
                default:
                    localobj.Message = "";
                    toIndex = false;
                    BindClass.playerId = returnedId;
                    break;
            }

            counterObj = -1;
            counterObj = obj.FindIndex((x) => x.Message == localobj.Message);
            if (counterObj == -1)
            {
                obj.Add(localobj);
                counterObj = obj.Count - 1;
            }

            if (toIndex)
                return RedirectPermanent("Index");
            else
                return RedirectToActionPermanent("ShowAccount", "Account");
        }

        public async void Serialize()
        {
            await Task.Run(() => DataBase.Serialize("PlayersFile", DataBase.ActivPlayers));
        }

        public IActionResult Log_on(string playername, string password)
        {
            Player player = new Player(playername, password, 1000);
            bool flag = DataBase.AddPlayer(player);
            AuthIndexViewModel localobj = new AuthIndexViewModel();
            if (flag)
            {
                localobj.Message = "Account was created";
                Serialize();
            }
            else localobj.Message = "This name was occupied";

            counterObj = -1;
            counterObj = obj.FindIndex((x) => x.Message == localobj.Message);
            if (counterObj == -1)
            {
                obj.Add(localobj);
                counterObj = obj.Count - 1;
            }

            return RedirectToAction("Index", "Auth");
        }
    }
}
