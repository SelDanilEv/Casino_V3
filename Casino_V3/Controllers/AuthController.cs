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

        public IActionResult Log_in (string playername,string password)
        {
            returnedId = DataBase.Authorization(playername, password);
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
                    BindClass.playerId = returnedId;
                    //return  RedirectToPage("Account");
                    return RedirectToActionPermanent("ShowAccount", "Account");
                    //return RedirectPermanent("/Account/ShowAccount");
            }
            return RedirectPermanent("Index");
        }

        public async void Serialize()
        {
            await Task.Run(() => DataBase.Serialize("PlayersFile", DataBase.ActivPlayers));
        }

        public IActionResult Log_on(string playername, string password)
        {
            Player player = new Player(playername, password, 1000);
            bool flag = DataBase.AddPlayer(player);
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
