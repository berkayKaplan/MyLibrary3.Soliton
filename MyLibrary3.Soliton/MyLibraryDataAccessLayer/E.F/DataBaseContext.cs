using MyLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryDataAccessLayer.E.F
{
    public class DataBaseContext:DbContext
    {
        public DbSet<LibraryUser> LibraryUser { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookNames> BookNames{ get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DataBaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
