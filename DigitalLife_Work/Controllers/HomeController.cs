using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DigitalLife_Work.Models;
using DigitalLife_Work.Models.VM;
using DigitalLife_Work.Models.DAL;
using Microsoft.EntityFrameworkCore;
using DigitalLife_Work.Models.BLL;

namespace DigitalLife_Work.Controllers
{
    //[Route("[controller]/[action]")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ProjectClientViewModelcs vm = new ProjectClientViewModelcs()
            {
                Invoice = new Invoice(),
                Invoices = await _context.Invoices.Include(c => c.Client).Include(p => p.Project).ToListAsync(),
                Clients = await _context.Clients.ToListAsync(),
                Projects = await _context.Projects.ToListAsync()
            };
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProjectClientViewModelcs vm = new ProjectClientViewModelcs()
            {
                Invoice = new Models.BLL.Invoice(),
                Clients = await _context.Clients.ToListAsync(),
                Projects = await _context.Projects.ToListAsync()
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectClientViewModelcs projectClientViewModelcs,
                                                                            int clientid, int statusid, int projectid)
        {
            if (ModelState.IsValid)
            {
                if (projectClientViewModelcs.Invoice == null)
                {
                    return NotFound();
                }
                var proje = await _context.Projects.FindAsync(projectid);
                var client = await _context.Clients.FindAsync(clientid);

                var status = ((PaymentStatus)statusid).ToString();

                Invoice invoice = new Invoice()
                {
                    NetAmount = projectClientViewModelcs.Invoice.NetAmount,
                    TaxAmount = projectClientViewModelcs.Invoice.TaxAmount,
                    TotalAmount = projectClientViewModelcs.Invoice.TotalAmount,
                    Note = projectClientViewModelcs.Invoice.Note,
                    DateTime = projectClientViewModelcs.Invoice.DateTime,
                    ProjectId = projectid,
                    ClientId = clientid,
                    Project = proje,
                    Client = client,
                    Status = status
                };

                await _context.Invoices.AddAsync(invoice);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            ProjectClientViewModelcs viewModel = new ProjectClientViewModelcs()
            {
                Clients = await _context.Clients.ToListAsync(),
                Invoice = invoice,
                Projects = await _context.Projects.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectClientViewModelcs projectClientViewModelcs, int clientid, int statusid, int projectid)
        {

            if (ModelState.IsValid)
            {
                if (projectClientViewModelcs.Invoice == null)
                {
                    return NotFound();
                }
                var proje = await _context.Projects.FindAsync(projectid);
                var client = await _context.Clients.FindAsync(clientid);

                var status = ((PaymentStatus)statusid).ToString();

                var invoiceInfo = await _context.Invoices.FindAsync(id);
                invoiceInfo.NetAmount = projectClientViewModelcs.Invoice.NetAmount;
                invoiceInfo.TaxAmount = projectClientViewModelcs.Invoice.TaxAmount;
                invoiceInfo.Note = projectClientViewModelcs.Invoice.Note;
                invoiceInfo.TotalAmount = projectClientViewModelcs.Invoice.TotalAmount;
                invoiceInfo.DateTime = projectClientViewModelcs.Invoice.DateTime;
                invoiceInfo.ProjectId = projectid;
                invoiceInfo.ClientId = clientid;
                invoiceInfo.Project = proje;
                invoiceInfo.Client = client;
                invoiceInfo.Status = status;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Filter(DateTime? date, int clientid, int projectid)
        {
            FilterViewModel viewModel = new FilterViewModel();
            if (date == DateTime.MinValue)
            {
                date = null;
                viewModel.DateTime = null;
            }
            else
            {
                viewModel.DateTime = date;
            }

            var client = await _context.Clients.FindAsync(clientid);
            if (client != null)
            {
                viewModel.ClientName = client.Name;
            }
            var project = await _context.Projects.FindAsync(projectid);
            if (project != null)
            {
                viewModel.ProjectName = project.Name;
            }


            var invoice = await _context.Invoices.Where(c => (viewModel.ClientName != null && viewModel.ProjectName == null && viewModel.DateTime == null) ? (c.Client.Name == viewModel.ClientName) : true
            && (viewModel.ClientName == null && viewModel.ProjectName != null && viewModel.DateTime == null) ? (c.Project.Name == viewModel.ProjectName) : true
            && (viewModel.DateTime != null && viewModel.ClientName == null && viewModel.ProjectName == null) ? (c.DateTime == date) : true
            && (viewModel.ClientName != null && viewModel.ProjectName != null && viewModel.DateTime == null) ? (c.Client.Name == viewModel.ClientName && c.Project.Name == viewModel.ProjectName ) : true
            && (viewModel.ClientName != null && viewModel.ProjectName == null && viewModel.DateTime != null) ? (c.Client.Name == viewModel.ClientName && c.DateTime == date) : true
            && (viewModel.ClientName == null && viewModel.ProjectName != null && viewModel.DateTime != null) ? (c.Project.Name == viewModel.ProjectName && c.DateTime == date) : true
            && (viewModel.ClientName != null && viewModel.ProjectName != null && viewModel.DateTime !=null) ? (c.Client.Name == viewModel.ClientName && c.Project.Name == viewModel.ProjectName && c.DateTime == date) : true

            ).ToListAsync();


            List<InvoiceViewModel> vm = new List<InvoiceViewModel>();
            foreach (var item in invoice)
            {
                InvoiceViewModel vm2 = new InvoiceViewModel();
                var invoiceProject = _context.Projects.Find(item.ProjectId);

                var invoiceClient = _context.Clients.Find(item.ClientId);

                vm2.Id = item.Id;
                vm2.TaxAmount = item.TaxAmount;
                vm2.Note = item.Note;
                vm2.NetAmount = item.NetAmount;
                vm2.ProjectId = item.ProjectId;
                vm2.TotalAmount = item.TotalAmount;
                vm2.DateTime = item.DateTime;
                vm2.Status = item.Status;
                vm2.ClientId = item.ClientId;
                vm2.Project = await _context.Projects.FindAsync(item.ProjectId);
                vm2.Client = await _context.Clients.FindAsync(item.ClientId);
                vm.Add(vm2);
            }

            if (invoice != null)
            {
                return Json(new
                {
                    status = 200,
                    data = vm
                });
            }

            return Json(new
            {
                status = 400
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
