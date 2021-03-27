using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.BLL
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int NetAmount { get; set; }
        [Required]
        public int TaxAmount { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        //foreign key  relations
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int ClientId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum PaymentStatus
    {
        pending = 1,
        done = 2

    }
}
