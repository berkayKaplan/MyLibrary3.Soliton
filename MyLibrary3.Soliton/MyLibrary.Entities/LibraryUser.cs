using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("LibraryUsers")]
    public class LibraryUser:MyEntitesBase
    {
        
        public string Name { get; set; }

        public string Surname { get; set; }
        
        public string Username { get; set; }
       
        public string Email { get; set; }
       
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
   
        public Guid ActivateGuid { get; set; }
        public virtual List<Examination> MyExaminations { get; set; }
        public virtual List<Comment> MyComments { get; set; }
        public virtual List<Liked> Likes { get; set; }
        public Guid ActiveteGuid { get; set; }

      
    }
}
