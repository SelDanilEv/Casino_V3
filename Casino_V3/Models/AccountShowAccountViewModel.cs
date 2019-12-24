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
            user = new User();
        }

        public User user;
    }
}
