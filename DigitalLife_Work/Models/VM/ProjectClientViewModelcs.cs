using DigitalLife_Work.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.VM
{
    public class ProjectClientViewModelcs
    {
        public Invoice Invoice { get; set; }
        public List<Invoice> Invoices { get; set; }

        public List<Project> Projects { get; set; }
        public List<Client> Clients { get; set; }
        

    }
}
