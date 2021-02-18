using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyNotebook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool? isAdmin { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<ToDo> ToDos { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
    }
}
