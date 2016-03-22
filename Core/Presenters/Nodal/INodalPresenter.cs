using code_in.Views.NodalView.NodesElems.Nodes;
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
        void EditFunction(FuncDeclNode node);
        //List<Type>  GetContextListNodes(NodePresenter context)
    }
}
