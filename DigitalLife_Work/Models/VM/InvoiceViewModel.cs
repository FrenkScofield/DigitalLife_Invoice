using DigitalLife_Work.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.VM
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int NetAmount { get; set; }
        public int TaxAmount { get; set; }
        public int TotalAmount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }


        //foreign key  relations
        public int ProjectId { get; set; }
        public int ClientId { get; set; }

        public Client Client { get; set; }
        public Project Project { get; set; }

    }
}
