using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino_V3.Models;
using Microsoft.AspNetCore.Http;

namespace Casino_V3.Controllers
{
    public class AccountController : Controller        //привести все в порядок согласование об изменении
    {
        private static List<AccountShowAccountViewModel> objShowAccount = new List<AccountShowAccountViewModel>() { };
        private static List<AccountStartViewModel> objStart = new List<AccountStartViewModel>() { };
        private static int counterObjShowAccount = -1;
        private static int counterObjStart = -1;


        public IActionResult ShowAccount()
        {
            int? PlayerId = BindClass.playerId;
            BindClass.playerId = null;

            AccountShowAccountViewModel localobjShowAccount = new AccountShowAccountViewModel();

            if (PlayerId != null)
            {
                localobjShowAccount.player = DataBase.ActivPlayers[(int)PlayerId];
                counterObjShowAccount = -1;
                counterObjShowAccount = objShowAccount.FindIndex((x) => x.player.Name == localobjShowAccount.player.Name);
                if (counterObjShowAccount == -1)
                {
                    objShowAccount.Add(localobjShowAccount);
                    counterObjShowAccount = objShowAccount.Count - 1;
                }
                objShowAccount[counterObjShowAccount] = localobjShowAccount;
                counterObjShowAccount = -1;
            }
            else
            {
                if (counterObjShowAccount >= 0)
                {
                    localobjShowAccount = objShowAccount[counterObjShowAccount];
                    localobjShowAccount.player = DataBase.ActivPlayers[localobjShowAccount.player.Id];
                    objShowAccount[counterObjShowAccount] = localobjShowAccount;
                    counterObjShowAccount=-1;
                }
                else
                    return RedirectToActionPermanent("Log_out", "Account");
            }
            return View(localobjShowAccount);
        }

        public IActionResult Start(int rate, int typeRate,int id)
        {
            AccountShowAccountViewModel localobjShowAccount = new AccountShowAccountViewModel();
            string name = DataBase.ActivPlayers[id].Name;
            counterObjShowAccount = objShowAccount.FindIndex((x) => x.player.Name == name);
            localobjShowAccount = objShowAccount[counterObjShowAccount];

            bool flag = false;
            if (rate > localobjShowAccount.player.Cash)
            {
                localobjShowAccount.Message = "Rate is too large for you";
                flag = true;
            }
            if (rate <= 0)
            {
                localobjShowAccount.Message = "Wrong rate";
                flag = true;
            }

            switch (typeRate)
            {
                default:
                    flag = true;
                    break;
                case 1:
                    localobjShowAccount.player.TypeOfRate = Player.TypeRate.zero;
                    break;
                case 2:
                    localobjShowAccount.player.TypeOfRate = Player.TypeRate.color;
                    break;
                case 3:
                    localobjShowAccount.player.TypeOfRate = Player.TypeRate.sector;
                    break;
            }
            if (flag)
            {
                counterObjShowAccount = objShowAccount.FindIndex((x) => x.player.Name == DataBase.ActivPlayers[id].Name);
                return RedirectPermanent("ShowAccount");
            }
            localobjShowAccount.player.Rate = rate;

            AccountStartViewModel localobjStart = new AccountStartViewModel(localobjShowAccount.player);
            counterObjStart = -1;
            counterObjStart = objStart.FindIndex((x) => x.player.Name == localobjStart.player.Name);
            if (counterObjStart == -1)
            {
                objStart.Add(localobjStart);
                counterObjStart = objStart.Count - 1;
            }
            return View(localobjStart);
        }

        public IActionResult Cancel(int id)
        {
            AccountShowAccountViewModel localobjShowAccount = new AccountShowAccountViewModel();
            string name = DataBase.ActivPlayers[id].Name;
            counterObjShowAccount = objShowAccount.FindIndex((x) => x.player.Name == name);
            localobjShowAccount = objShowAccount[counterObjShowAccount];
            localobjShowAccount.player.Rate = 0;
            localobjShowAccount.player.TypeOfRate = Player.TypeRate.noth;
            counterObjShowAccount = objShowAccount.FindIndex((x) => x.player.Name == DataBase.ActivPlayers[id].Name);
            objShowAccount[counterObjShowAccount] = localobjShowAccount;
            return RedirectPermanent("ShowAccount");
        }

        public IActionResult Log_out()
        {
            return RedirectToActionPermanent("Log_out", "Auth");
        }
    }
}