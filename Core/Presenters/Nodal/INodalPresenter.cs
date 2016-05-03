using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Presenters.Nodal
{
    public interface INodalPresenter // For future network abstraction
    {
        void OpenFile(String path);
        void EditFunction(FuncDeclItem node);
        Tuple<EContextMenuOptions, HexagonalButton.ButtonAction>[] GetMenuOptions();
        //List<Type>  GetContextListNodes(NodePresenter context)
    }

    public enum EContextMenuOptions
    {
        REMOVE,
        EDIT,
        GOINTO,
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
}
