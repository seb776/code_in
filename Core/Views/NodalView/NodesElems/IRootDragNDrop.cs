using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems
{
    public interface IRootDragNDrop : IVisualNodeContainerDragNDrop
    {
        void SelectNode(INodeElem node);
        void UnSelectNode(INodeElem node);
        void RevertChange(); // Resets nodes to their original positions. To be used when a DragNDrop is not valid.
        void UnSelectAllNodes();
        void DragNodes();
        void DropNodes(IVisualNodeContainerDragNDrop container);
        void UpdateDragState(Point pos); // This function is here to update the view when mouse is moving (update view and links)
    }
}
