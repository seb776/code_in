using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.Nodes;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public interface IVisualNodeContainer
    {
        T CreateAndAddNode<T>() where T : UIElement, INodeElem;
        void AddNode<T>(T node) where T : UIElement, INodeElem;
        //void RemoveNode(INode node);
    }
}
