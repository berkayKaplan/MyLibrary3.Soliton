using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("Examination")]
    public class Examination:MyEntitesBase
    {
        public LibraryUser ModifiedUserName;

        
        public string Title { get; set; }
       
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        public int CategoryId { get; set; }
        public virtual LibraryUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Liked> Likes { get; set; }
        public virtual BookNames BookNames { get; set; }
        public object ModifieUserName { get; set; }
        public Examination()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
      
           
        }


    }
}
