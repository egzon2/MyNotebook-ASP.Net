using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotebook.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }

        public bool Completed { get; set; }
        public bool Priority { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
    }
}
