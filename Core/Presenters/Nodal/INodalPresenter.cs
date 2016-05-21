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
    public interface INodalPresenter : IContextMenu // For future network abstraction
    {
        void OpenFile(String path);
        void SaveFile(String path);
        void EditFunction(FuncDeclItem node);
        // void EditProperty(object obj); // TODO @z0rg type is not created yet
    }


}
