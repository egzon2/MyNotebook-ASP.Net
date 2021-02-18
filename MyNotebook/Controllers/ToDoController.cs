using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MyNotebook.Data;
using MyNotebook.Models;


namespace MyNotebook.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ToDoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpGet("{id}")]
        [Route("ToDo")]
        [Route("ToDo/Index")]
        [Authorize]
        public async Task<IActionResult> Index( int? pageNumber, string tick)
        {
            var UserId = userManager.GetUserId(HttpContext.User);

            var todos = from s in _context.ToDo
                        select s;

            todos = todos.Where(s => Equals(s.UserId, UserId));

            if (tick !=null) {
                if (Equals(tick, "Completed")) {
                    todos = todos.Where(s => s.Completed == true);
                }
                else {
                    todos = todos.Where(s => s.Completed == false);
                }
            }
            
           

            int pageSize = 5;
            return View(await PaginatedList<ToDo>.CreateAsync(todos.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDo/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.userId = userManager.GetUserId(HttpContext.User);
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Completed,Priority,CreatedAt")] ToDo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: ToDo/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", toDo.UserId);
            return View(toDo);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Title,Completed,Priority,CreatedAt")] ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", toDo.UserId);
            return View(toDo);
        }

        // GET: ToDo/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var toDo = await _context.ToDo.FindAsync(id);
            _context.ToDo.Remove(toDo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(Guid id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }
    }
}
