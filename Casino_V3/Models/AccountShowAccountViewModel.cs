﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino_V3.Models
{
    public class AccountShowAccountViewModel
    {
        public AccountShowAccountViewModel()
        {
            player = new Player();
        }

        public Player player;
        public string Message = "";
    }
}
