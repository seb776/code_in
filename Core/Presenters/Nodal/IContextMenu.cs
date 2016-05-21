using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal
{
    public enum EContextMenuOptions
    {
        REMOVE,
        EDIT, // To open a local modification panel
        GOINTO, // Property of method definition
        EXPAND,
        COLLAPSE,
        EXPANDALL,
        COLLAPSEALL,
        HELP,
        ADD,
        ALIGN,
        SAVE,
        CLOSE,
        DUPLICATE
    }
    /// <summary>
    /// This class has to be inherited by each presenter corresponding to a view in which we have a contextMenu.
    /// It allows to get available operations depending on the context we are asking.
    /// </summary>
    public interface IContextMenu
    {
        Tuple<EContextMenuOptions, Action<object[]>>[] GetMenuOptions();
    }
}
