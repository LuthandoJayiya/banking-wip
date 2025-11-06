using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Column("transaction_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Column("account_id")]
        public Guid AccountId { get; set; }
        
        [Column("account_type"), Required]
        public string AccountType { get; set; }

        [Column("customer_id"), Required]
        public int CustomerId { get; set; }


    }
}
