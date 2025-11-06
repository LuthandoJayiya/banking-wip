using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.Models
{
    [Table("messages")]
    public class Message
    {
        [Key, Column("message_id")]
        public int MessageId { get; set; }

        [Column("email_message")]
        public string EmailMessage { get; set; }
        
        [Column("log_message")]
        public string LogMessage { get; set; }

        [Column("customer_id"), Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
