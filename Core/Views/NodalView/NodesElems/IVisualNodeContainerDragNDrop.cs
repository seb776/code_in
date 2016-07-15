using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems
{
    public interface IVisualNodeContainerDragNDrop : IVisualNodeContainer
    {
        // IVisualNodeContainerDragNDrop
        bool IsDropNodeValid();
        int GetDropNodeIndex(Point pos); // Gets the index where the element will be pushed
        void HighLightDropNodePlace(Point pos); // Displays a visual element to show where the node will be dropped
    }

    struct VisualNodeInteractionState
    {
        enum EInteractionType
        {
            NONE = 0,
            RESIZE = 1,
            LINE = 2,
            MOVE = 3
        }
        List<INodeElem> SelectedNodes;
        EInteractionType InteractionType; // Create link, move node... (ancient TransformationMode)
        // For MOVE
        private Point _newNodePos;
        private Point _lastPosition;
        // For link creation
        Code_inLink _currentDrawingLink;
    }
}
