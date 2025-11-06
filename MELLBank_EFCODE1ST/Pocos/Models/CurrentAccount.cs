using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("current_accounts")]
    public class CurrentAccount
    {
        [Key]
        [Column("current_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CurrentId { get; set; }

        [Column("account_number")]
        public string AccountNumber { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("balance")]
        public double Balance { get; set; }

        [Column("branch_code")]
        public string BranchCode { get; set; }

        [Column("overdraft_amount")]
        public double OverdraftAmount { get; set; }

        [Column("overdraft_rate")]
        public double OverdraftRate { get; set; }

        [Column("close_date")]
        public DateTime? CloseDate { get; set; }


        [Column("customer_id"), Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
