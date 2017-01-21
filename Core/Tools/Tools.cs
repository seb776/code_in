using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace code_in.Tools
{
    public static class Tools
    {
        public static List<Type> GetAllSubclassesOf(Type baseType) { return Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType)).ToList(); }
        public static IEnumerable<T> FindVisualChildren<T>(System.Windows.DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
    public static class StringExtensions
    {
        public static int Contains(this string source, string toCheck, bool caseSensitive)
        {
            if (source == null)
                return -1;
            StringComparison comp = (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return source.IndexOf(toCheck, comp);
        }
    }

}
