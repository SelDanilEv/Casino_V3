using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino_V3.Models
{
    public class AccountStartViewModel
    {
        public AccountStartViewModel(Player play)
        {
            player = play;
        }
        
        public Player player;
        public string Message = "";
    }
}
