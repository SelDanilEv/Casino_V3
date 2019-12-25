using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino_V3.Models;
using Microsoft.AspNetCore.Http;

namespace Casino_V3.Controllers
{
    public class AccountController : Controller
    {
        private static AccountShowAccountViewModel objShowAccount;
        private static AccountStartViewModel objStart;

        public int? PlayerId;

        public IActionResult ShowAccount()
        {
            PlayerId = BindClass.playerId;
            BindClass.playerId = null;

            if (PlayerId != null)
            {
                objShowAccount = new AccountShowAccountViewModel();
                objShowAccount.player = DataBase.ActivPlayers[(int)PlayerId];
            }
            else
            {
                if (objShowAccount == null)
                    return RedirectToActionPermanent("Log_out", "Account");
            }
            return View(objShowAccount);
        }

        public IActionResult Start(int rate, int typeRate)
        {
            bool flag = false;
            if (rate > objShowAccount.player.Cash)
            {
                objShowAccount.Message = "Rate is too large for you";
                flag = true;
            }
            if (rate <= 0)
            {
                objShowAccount.Message = "Wrong rate";
                flag = true;
            }

            switch (typeRate)
            {
                default:
                    flag = true;
                    break;
                case 1:
                    objShowAccount.player.TypeOfRate = Player.TypeRate.zero;
                    break;
                case 2:
                    objShowAccount.player.TypeOfRate = Player.TypeRate.color;
                    break;
                case 3:
                    objShowAccount.player.TypeOfRate = Player.TypeRate.sector;
                    break;
            }
            if (flag)
                return RedirectPermanent("ShowAccount");
            objStart = new AccountStartViewModel(objShowAccount.player);
            return View(objStart);
        }

        public IActionResult Cancel()
        {
            objStart = null;
            objShowAccount.player.Rate = 0;
            objShowAccount.player.TypeOfRate = Player.TypeRate.noth;
            return RedirectPermanent("ShowAccount");
        }

        public IActionResult Log_out()
        {
            objShowAccount = null;
            return RedirectToActionPermanent("Index", "Auth");
        }
    }
}