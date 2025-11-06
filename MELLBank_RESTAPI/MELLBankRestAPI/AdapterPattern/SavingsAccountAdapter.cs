using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;

namespace MELLBankRestAPI.AdapterPattern
{
    public class SavingsAccountAdapter : IModelAdapter<SavingsAccountVM, SavingsAccount>
    {
        public SavingsAccountVM ConvertModelToViewModel(SavingsAccount model)
        {
            return new SavingsAccountVM
            {
                AccountNumber = model.AccountNumber,
                CreationDate = model.CreationDate,
                Balance = model.Balance,
                BranchCode = model.BranchCode,
                CloseDate = model.CloseDate,
                InterestRateId = model.InterestRateId,
                CustomerId = model.CustomerId
            };
        }

        public SavingsAccount ConvertViewModelToModel(SavingsAccountVM viewModel)
        {
            return new SavingsAccount
            {
                AccountNumber = viewModel.AccountNumber,
                CreationDate = viewModel.CreationDate,
                Balance = viewModel.Balance,
                BranchCode = viewModel.BranchCode,
                CloseDate = viewModel.CloseDate,
                InterestRateId = viewModel.InterestRateId,
                CustomerId = viewModel.CustomerId
            };
        }
    }
}
