using MELLBankRestAPI.Controllers;
using MELLBankRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Tests.Controllers
{
    internal class SavingsAccountControllerTests
    {
        private Object _accountContext;
        private SavingsAccountController _controllerUnderTest;
        private List<SavingsAccount> _accList;

        SavingsAccount _account;

        [SetUp]
        public void Init()
        {
            _accountContext = InMemoryContext.GeneratedFurnitureStoreDBContext();
            _accList = new List<SavingsAccount>();
            _account = new SavingsAccount()
            {
                SavingsId= Guid.NewGuid(),
                CustomerId = 1,
                AccountNumber="7564644432",
                Balance=0,
        };
   }


        [TearDown]
        public void CleanUp()
        {
            _accountContext = null;
            _controllerUnderTest = null;
            _accList = null;
            _account = null;
        }
    }
}

