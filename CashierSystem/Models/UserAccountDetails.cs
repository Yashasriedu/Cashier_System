using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashierSystem.Entities
{
    public class UserAccountDetails
    {
     public UserAccount AccountDetails { get; set; }
     public List<TransactionHistroy> ListOfTransactions { get; set; }
        public bool IsWithDraw = false;
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class TransactionHistroy
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public bool IsWithDraw { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public DateTime TransactionDate { get; set; }
    }
   
}