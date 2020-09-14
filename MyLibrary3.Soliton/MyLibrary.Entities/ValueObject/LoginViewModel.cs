using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLibrary3.Entites.ValueObject
{
    public class LoginViewModel
    {
      
        public  string Username { get; set; }
        
        public string Password { get; set; }
    }
}