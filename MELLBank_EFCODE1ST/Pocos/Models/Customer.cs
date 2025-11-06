using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key, Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("first_name"), Required]
        public string FirstName { get; set; }

        [Column("last_name"), Required]
        public string LastName { get; set; }

        [Column("username"), Required]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number"), MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Column("street_address"), MaxLength(100)]
        public string StreetAddress { get; set; }

        [Column("city"), MaxLength(100)]
        public string City { get; set; }

        [Column("province"), MaxLength(100)]
        public string Province { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }

        public virtual ICollection<SavingsAccount> SavingsAccounts { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }
}
