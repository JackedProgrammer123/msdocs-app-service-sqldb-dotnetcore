using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly MyDatabaseContext _context;

        public PropertiesController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var Properties = new List<Property>();

            // This allows the home page to load if migrations have not been run yet.
            try
            {
                Properties = await _context.Property.ToListAsync();
            }
            catch (Exception e)
            {

                return View(Properties);
            }

            return View(Properties);
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Property = await _context.Property
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Property == null)
            {
                return NotFound();
            }

            return View(Property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,CreatedDate")] Property Property)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Property = await _context.Property.FindAsync(id);
            if (Property == null)
            {
                return NotFound();
            }
            return View(Property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,Location,Cost,CreatedDate")] Property Property)
        {
            if (id != Property.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(Property.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Property = await _context.Property
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Property == null)
            {
                return NotFound();
            }

            return View(Property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Property = await _context.Property.FindAsync(id);
            _context.Property.Remove(Property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.ID == id);
        }
    }
}
