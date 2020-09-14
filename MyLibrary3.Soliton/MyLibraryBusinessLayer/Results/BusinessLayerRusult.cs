using MyLibrary.Entities.Masseges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryBusinessLayer.Results
{
    public class BusinessLayerRusult<T> where T: class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerRusult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
        }
    }
}
