using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Entities
{
    [Table("BookNames")]
    public class BookNames:MyEntitesBase
    {
        public string Title;
        public Category Category;
        public LibraryUser Owner;
        public LibraryUser ModifiedUserName;
        public bool IsDraft;

        public string Text { get; set; }
        public byte[] Picture { get; set; }

       
    }
}
