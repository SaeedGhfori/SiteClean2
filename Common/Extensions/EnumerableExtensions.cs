using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EnumerableExtensions
    {
        // تبدیل یک IEnumerable به یک HashSet برای بهبود عملکرد جستجو
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }

}
