using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotebook.Models
{
    public class Reminder
    {
        public Guid ReminderId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }

        public DateTime? Date { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
    }
}
