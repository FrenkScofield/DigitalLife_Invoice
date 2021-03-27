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
    public class ProjeController : Controller
    {
        private readonly MyContext _context;

        public ProjeController(MyContext context)
        {
            _context = context;
        }
        //Project index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }
        //Project index Function End

        //Project Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.AddAsync(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //Project Create Function End

        //Project Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Ptoje = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (Ptoje == null)
            {
                return NotFound();
            }
            return View(Ptoje);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(project);
            }
            var ProProject = await _context.Projects.FindAsync(id);

            ProProject.Name = project.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //Project Edit Function End

        //Project Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Project Delete Function End

    }
}
