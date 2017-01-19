using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Tools
{
    public static class Tools
    {
        public static List<Type> GetAllSubclassesOf(Type baseType) { return Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType)).ToList(); }
    }
    public static class StringExtensions
    {
        public static int Contains(this string source, string toCheck, bool caseSensitive)
        {
            StringComparison comp = (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return source.IndexOf(toCheck, comp);
        }
    }
}
