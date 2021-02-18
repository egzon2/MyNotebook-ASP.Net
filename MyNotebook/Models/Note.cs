using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotebook.Models
{
    public class Note
    {
        public Guid NoteId { get; set; }
       
        public string UserId { get; set; }  
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedAt{ get; set; }

        public ApplicationUser User { get; set; }
    }
}
