using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("transaction_details")]
    public  class TransactionDetail
    {

        [Key, Column("trans_detail_id")]
        public int TransDetailId { get; set; }

        [Column("transaction_date"), Required]
        public DateTime TransactionDate { get; set; }

        [Column("amount"), Required]
        public double Amount { get; set; }

        [Column("transaction_type"), Required]
        public string TransactionType { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("transaction_id")]
        public int TransactionId { get; set; }
        
    }
}
