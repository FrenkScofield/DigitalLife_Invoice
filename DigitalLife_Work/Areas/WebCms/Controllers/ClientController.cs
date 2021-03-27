using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLife_Work.Models.BLL;
using DigitalLife_Work.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalLife_Work.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    // [Route("WebCms/")]
    [Route("WebCms/[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly MyContext _context;

        public ClientController(MyContext context)
        {
            _context = context;
        }
        //Client index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }
        //Client index Function End

        //Client Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.AddAsync(client);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //Client Create Function End

        //Client Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }
            var ProClient = await _context.Clients.FindAsync(id);

            ProClient.Name = client.Name;
            ProClient.Surname = client.Surname;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //Client Edit Function End

        //Client Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Client Delete Function End

    }
}

