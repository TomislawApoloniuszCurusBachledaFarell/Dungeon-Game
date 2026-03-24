using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault_Scavanger.Classes.Utilis;

public static class ListExtensions
{
    public static T? GetAndRemove<T>(this List<T> list, int index)
    {
        if(index < 0 || index >= list.Count)
            return default;
        T? result = list[index];
        list.RemoveAt(index);
        return result;
    }
}
