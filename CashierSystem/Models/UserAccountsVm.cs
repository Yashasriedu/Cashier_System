using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashierSystem.Entities
{
    public class UserAccount
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Balance { get; set; }
    }

    public class UserAccountsVm { 
        public List<UserAccount> UserAccounts { get; set; }
    }
}