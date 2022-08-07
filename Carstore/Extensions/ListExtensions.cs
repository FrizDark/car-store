using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carstore.Extensions
{
    public static class ListExtensions
    {

        public static List<T> SearchInComboBox<T>(this List<T> items, ref string search, char c, Func<T, bool> searchAction)
        {
            if (c == 8)
            {
                if (search.Length > 0)
                    search = search.Remove(search.Length - 1);
            }
            else
            {
                search += c;
            }
            return items.Where(searchAction).ToList();
        }

    }
}
