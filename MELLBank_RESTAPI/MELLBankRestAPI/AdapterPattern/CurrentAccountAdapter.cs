using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;

namespace MELLBankRestAPI.AdapterPattern
{
    public class CurrentAccountAdapter : IModelAdapter<CurrentAccountVM, CurrentAccount>
    {
        public CurrentAccountVM ConvertModelToViewModel(CurrentAccount model)
        {
            return new CurrentAccountVM
            {
                CurrentId = model.CurrentId,
                CustomerId = model.CustomerId,
                AccountNumber = model.AccountNumber,
                CreationDate = model.CreationDate,
                Balance = model.Balance,
                BranchCode = model.BranchCode,
                OverdraftAmount = model.OverdraftAmount,
                OverdraftRate = model.OverdraftRate,
                CloseDate = model?.CloseDate
            };

        }
        public CurrentAccount ConvertViewModelToModel(CurrentAccountVM viewModel)
        {
            return new CurrentAccount
            {
                CurrentId = viewModel.CurrentId,
                CustomerId = viewModel.CustomerId,
                AccountNumber = viewModel.AccountNumber,
                CreationDate = viewModel.CreationDate,
                Balance = viewModel.Balance,
                BranchCode = viewModel.BranchCode,
                OverdraftAmount = viewModel.OverdraftAmount,
                OverdraftRate = viewModel.OverdraftRate,
                CloseDate = viewModel?.CloseDate
            };
        }
    }
}