using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("Likes")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
             
        public int Id { get; set; }
        public Examination Examination { get; set; }
        public LibraryUser LikedUser { get; set; }
    }
}
