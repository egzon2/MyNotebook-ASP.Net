using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotebook.Models
{
    public class EditUserViewModel
    {
        public string UserName { get; set; }
        public bool? isAdmin { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<ToDo> ToDos { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
    }
}
