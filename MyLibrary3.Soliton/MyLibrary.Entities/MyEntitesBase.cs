using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    public class MyEntitesBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public DateTime CreatedOn { get; set; }
     
        public DateTime ModifieOn { get; set; }
      
        public string ModifieOnUserName { get; set; }
    }
}
