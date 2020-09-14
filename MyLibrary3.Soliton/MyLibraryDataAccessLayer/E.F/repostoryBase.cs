using MyLibraryDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryDataAccessLayer.E.F
{
    public class repostoryBase
    {
        private static DataBaseContext _db;
        protected repostoryBase()
        {

        }
        public static DataBaseContext CreateContext()
        {
            if (_db==null)
            {
                _db = new DataBaseContext();
            }
            return _db;
        }
    }
}
