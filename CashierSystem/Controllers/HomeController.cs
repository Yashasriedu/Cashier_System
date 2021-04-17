using CashierSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashierSystem.Controllers
{
    public class HomeController : Controller
    {
        private DataAccessLayer _dataAccessLayer;
        public HomeController()
        {
            _dataAccessLayer = new DataAccessLayer();
        }
        public ActionResult UserAccounts()
        {

            var model = new UserAccountsVm();
            var userAccounts = new List<UserAccount>();
            model.UserAccounts = _dataAccessLayer.GetAllAccounts();
            return View("UserAccounts", model);
        }
        public ActionResult AddAccount()
        {
            return View("AddAccount");
        }

        [HttpPost]
        public ActionResult AddAccount(string txtFirstName, string txtLastName, string txtAccountBalance)
        {
            var createAccountResult = _dataAccessLayer.CreateAccount(txtFirstName, txtLastName, 0);
            return RedirectToAction("UserAccounts", "Home");
        }
        [HttpPost]
        public ActionResult CreateTransaction(int accountId, int amount, bool isWithDraw)
        {
            var createAccountResult = _dataAccessLayer.CreateTransaction(accountId, amount, isWithDraw);
            return RedirectToAction("UserAccounts", "Home");
        }
        [HttpGet]
        public ActionResult GetUserAccountDetails(int accountId = 0)
        {

            var model = new UserAccountDetails();
            model.AccountDetails = _dataAccessLayer.GetAccountDetails(accountId);
            model.ListOfTransactions = _dataAccessLayer.GetAccountTransactions(accountId);
            return View("UserAccountDetails", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}