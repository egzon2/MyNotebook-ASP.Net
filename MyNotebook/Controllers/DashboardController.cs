using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using MyNotebook.Data;
using Microsoft.AspNetCore.Identity;
using MyNotebook.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotebook.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;

        }



        // GET: /<controller>/
        [Route("")]
        [Route("Dashboard")]
        [Route("Dashboard/Index")]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var UserId = userManager.GetUserId(HttpContext.User);

            var reminders = from s in _context.Reminder
                            select s;
            reminders = reminders.Where(s => Equals(s.UserId, UserId));

            ViewBag.ReminderCount = reminders.Count();

            var todos = from s in _context.ToDo
                        select s;
            reminders = reminders.Where(s => Equals(s.UserId, UserId));

            ViewBag.TodoCount = todos.Count();
            ViewBag.PriorityCount = todos.Where(s => s.Priority == true).Count();

            var notes = from s in _context.Note
                        select s;
            reminders = reminders.Where(s => Equals(s.UserId, UserId));

            ViewBag.NoteCount = notes.Count();



            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.kanye.rest/?format=text"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //reservationList = JsonConvert.DeserializeObject<List<String>>(apiResponse);
                    ViewData["Message"] = apiResponse; 
                    
                    return View();
                }
            }

        }
    }
}