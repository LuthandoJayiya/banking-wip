using MELLBankRestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Tests
{
    public class InMemoryContext
    {
        public static MELLBank_EFDBContext GeneratedFurnitureStoreDBContext()
        {

            var _contextOptions = new DbContextOptionsBuilder<MELLBank_EFDBContext>()
                .UseInMemoryDatabase("ControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new MELLBank_EFDBContext(_contextOptions);
        }
    }
}
