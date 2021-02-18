using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNotebook.Data;
using MyNotebook.Models;

namespace MyNotebook.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public NoteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [Route("Note")]
        [Route("Note/Index")]


        [Authorize]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            var UserId = userManager.GetUserId(HttpContext.User);
           

            var notes = from s in _context.Note
                        select s;

            notes = notes.Where(s => Equals(s.UserId, UserId));

            if (searchString != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    notes = notes.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()) || s.Message.ToUpper().Contains(searchString.ToUpper()));
                }
               
            }
           
            int pageSize = 3;
            return View(await PaginatedList<Note>.CreateAsync(notes.AsNoTracking(), pageNumber ?? 1, pageSize));

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
        public async Task<IActionResult> Create([Bind("NoteId,UserId,Title,Message,CreatedAt")] Note Note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Note model)
        {
            var note = await _context.Note.FindAsync(model.NoteId);

            if (note == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.NoteId} cannot be found";
                return View("NotFound");
            }
            else
            {
                note.Title = model.Title;
                note.Message = model.Message;
                note.CreatedAt = model.CreatedAt;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var result = _context.Update(note);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!NoteExists(note.NoteId))
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
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(Guid id) {

            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
           
        }
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var note = await _context.Note.FindAsync(id);

            if (note == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = _context.Remove(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
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
        }


        [Authorize]
        private bool NoteExists(Guid id)
        {
            return _context.Note.Any(e => e.NoteId == id);
        }
    }
}