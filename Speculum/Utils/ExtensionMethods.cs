using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Utils
{
    public static class ExtensionMethods
    {
        public static string ToUrlName(this string controllerName)
        {
            return controllerName.Replace("Controller", string.Empty);
        }
    }
}
