using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.NodesElems;
using System.Windows;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;

namespace code_in.Views.NodalView.NodesElems
{
    public enum EIOOption
    {
        NONE,
        INPUT,
        OUTPUT
    }
    public interface IVisualNodeContainer
    {
        T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, INodeElem;
        void AddNode<T>(T noden, int idx = -1) where T : UIElement, INodeElem;
        void RemoveNode(INodeElem node);
        int GetDropIndex(Point pos); // Gets the index where the element will be pushed
        void HighLightDropPlace(Point pos); // Displays a visual element to show where the node will be dropped
    }
}
