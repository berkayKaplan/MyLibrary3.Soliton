using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("Comments")]
    public class Comment:MyEntitesBase
    {
        public LibraryUser ModifiedUserName;

        
        public string Text { get; set; }
        public virtual Examination Examination { get; set; }
        public virtual LibraryUser Owner { get; set; }

      
    }
}
