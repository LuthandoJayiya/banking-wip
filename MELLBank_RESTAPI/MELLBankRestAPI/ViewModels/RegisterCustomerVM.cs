namespace MELLBankRestAPI.ViewModels
{
    public class RegisterCustomerVM
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }

        public bool CreateSavingsAccount { get; set; }
        public bool CreateCurrentAccount { get; set; }
    }
}
