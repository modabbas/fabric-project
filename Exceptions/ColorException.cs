using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class ColorException : BaseException
    {
        public override string Message
        {
            get { return "Please Check Your Color Repositpory The is  Error ! "; }
        }
    }
}
