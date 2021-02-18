using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyNotebook.Data;
using MyNotebook.Models;

namespace MyNotebook.Controllers
{
    [Authorize]
    public class ReminderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ReminderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [Authorize]
        [Route("Reminder")]
        [Route("Reminder/Index")]
        public async Task<IActionResult> Index(int? pageNumber, string tick)
        {
            var UserId = userManager.GetUserId(HttpContext.User);

            var reminders = from s in _context.Reminder
                        select s;
            reminders = reminders.Where(s => Equals(s.UserId, UserId));

            switch (tick)
            {
                
                case "Date":
                    reminders = reminders.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    reminders = reminders.OrderByDescending(s => s.Date);
                    break;
                default:
                    reminders = reminders.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Reminder>.CreateAsync(reminders.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Reminder/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminder
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReminderId == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.userId = userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ReminderId,UserId,Title,Date,CreatedAt")] Reminder Reminder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Reminder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminder.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", reminder.UserId);
            return View(reminder);
        }

        // POST: Reminder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReminderId,UserId,Title,Date,CreatedAt")] Reminder reminder)
        {
            if (id != reminder.ReminderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReminderExists(reminder.ReminderId))
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
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", reminder.UserId);
            return View(reminder);
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminder
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReminderId == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // POST: Reminder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reminder = await _context.Reminder.FindAsync(id);
            _context.Reminder.Remove(reminder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool ReminderExists(Guid id)
        {
            return _context.Reminder.Any(e => e.ReminderId == id);
        }
    }
}
