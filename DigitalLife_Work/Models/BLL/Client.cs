using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.BLL
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        //invoice relation
        //public virtual List<Invoice> Invoices { get; set; }

    }
}
