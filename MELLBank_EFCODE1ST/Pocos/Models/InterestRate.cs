using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("interest_rates")]
    public class InterestRate
    {
        [Key, Column("interest_rate_id")]
        public int InterestRateId { get; set; }

        [Column("interest")]
        public double Interest { get; set; }

        public virtual ICollection<SavingsAccount> SavingsAccounts { get; set; }
    }
}
