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
        private static AccountShowAccountViewModel obj = new AccountShowAccountViewModel();

        public int? UserId;

        public IActionResult ShowAccount()
        {
            UserId = BindClass.userId;
            BindClass.userId = null;
            if (UserId != null)
            {
                obj.user = DataBase.ActivUsers[(int)UserId];
            }
            else
            {
                return RedirectToActionPermanent("Log_out", "Account");
            }
            return View(obj);
        }

        public IActionResult Log_out()
        {
            obj.user.CorrectTransition = false;
            obj.user = null;
            return RedirectToActionPermanent("Index", "Auth");
        }
    }
}