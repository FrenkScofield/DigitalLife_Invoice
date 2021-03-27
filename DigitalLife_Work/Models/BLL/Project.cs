using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.BLL
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //invoice relation
        //public virtual List<Invoice> Invoices { get; set; }
    }
}
