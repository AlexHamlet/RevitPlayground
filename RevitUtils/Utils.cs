using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitUtils
{
    public static class Utils
    {
        public static T CheckNotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            return value;
        }
    }
}
