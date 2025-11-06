using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MELLBankRestAPI.AuthModels
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }
    }
}
