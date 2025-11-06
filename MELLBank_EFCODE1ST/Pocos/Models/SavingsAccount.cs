using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("savings_accounts")]
    public class SavingsAccount
    {
        [Key]
        [Column("savings_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SavingsId { get; set; }
        
        [Column("account_number")]
        public string AccountNumber { get; set; }
        
        [Column("creation_date")]
        public DateTime CreationDate { get; set; } 
        
        [Column("balance")]
        public double Balance { get; set; }
        
        [Column("branch_code")]
        public string BranchCode { get; set; }  
        
        [Column("close_date")]
        public DateTime? CloseDate { get; set; }

        [Column("interest_rate_id"), ForeignKey("InterestRate")]
        public int InterestRateId {  get; set; }

        public virtual InterestRate InterestRate { get; set; }

        [Column("customer_id"), Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
