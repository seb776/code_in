using code_in.Presenters.Nodal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    public interface IVisualNodeContainerDragNDrop : IVisualNodeContainer
    {
        void SelectNode(INodeElem node);
        void UnSelectNode(INodeElem node);
        void UnSelectAll();
        void DragNodes(TransformationMode transform, INodeElem node, LineMode lm);
        void DropNodes(INodeElem container);
    }
    public enum TransformationMode
    {
        NONE = 0,
        RESIZE,
        LINE,
        MOVE
    }

    public enum LineMode
    {
        NONE = 0,
        LINE = 1,
        SQUARE = 2,
        BEZIER = 3
    }
}
