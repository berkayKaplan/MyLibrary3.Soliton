using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("Catagories")]
    public class Category:MyEntitesBase
    {
        
        public string Title { get; set; }
       
        public string Description { get; set; }
        public virtual List<Examination> MyExaminations { get; set; }
        public Category()
        {
            MyExaminations = new List<Examination>();
        }

    }
}
