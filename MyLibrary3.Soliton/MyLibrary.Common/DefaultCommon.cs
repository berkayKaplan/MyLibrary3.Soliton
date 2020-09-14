using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Common
{
    public class DefaultCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            return "sytem";
        }
    }
}
