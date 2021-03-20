using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlighter_Mod_Helper.Api.Web
{
    public class ModUpdateChecker
    {
        public bool CheckForUpdate(string url)
        {
            var result = RestHelper.Get(url);
            Console.WriteLine(result);
            return false;
        }
    }
}
