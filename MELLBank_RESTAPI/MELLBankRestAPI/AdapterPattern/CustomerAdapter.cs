using MELLBankRestAPI.Models;
using MELLBankRestAPI.ViewModels;

namespace MELLBankRestAPI.AdapterPattern
{
    public class CustomerAdapter : IModelAdapter<CustomerVM, Customer>
    {
        public CustomerVM ConvertModelToViewModel(Customer model)
        {
            return new CustomerVM
            {
                CustomerId = model.CustomerId,
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                StreetAddress = model.StreetAddress,
                City = model.City,
                Province = model.Province,
                PostalCode = model.PostalCode,
            };
        }

        public Customer ConvertViewModelToModel(CustomerVM viewModel)
        {
            return new Customer
            {
                CustomerId = viewModel.CustomerId,
                Username = viewModel.Username ?? string.Empty,
                FirstName = viewModel.FirstName ?? string.Empty,
                LastName = viewModel.LastName ?? string.Empty,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                StreetAddress = viewModel.StreetAddress,
                City = viewModel.City,
                Province = viewModel.Province,
                PostalCode = viewModel.PostalCode,
            };
        }
        public Customer ConvertViewModelToModel(RegisterCustomerVM viewModel)
        {
            return new Customer
            {
                CustomerId = viewModel.CustomerId,
                Username = viewModel.Username ?? string.Empty,
                FirstName = viewModel.FirstName ?? string.Empty,
                LastName = viewModel.LastName ?? string.Empty,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                StreetAddress = viewModel.StreetAddress,
                City = viewModel.City,
                Province = viewModel.Province,
                PostalCode = viewModel.PostalCode,
            };
        }
    }
}
