using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView
{
    public interface IVisualNodeContainer
    {
        T AddNode<T>() where T : code_in.Views.MainView.Nodes.BaseNode;
        //void AddNode(Nodes.BaseNode node);
        //void RemoveNode(Nodes.BaseNode node);
    }
}
