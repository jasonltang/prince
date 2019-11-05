using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prince.ExtensionMethods
{
    public static class Util
    {
        public static readonly Random Random = new Random((int)DateTime.Now.Ticks);
    }
}
