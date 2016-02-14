using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    public interface IVisualNodeContainerDragNDrop
    {
        void SelectNode(INodeElem node);
        void UnSelectNode(INodeElem node);
        void UnSelectAll();
        void DragNodes(TransformationMode transform, INodeElem node);
        void DropNodes(IVisualNodeContainer container);
    }
    public enum TransformationMode
    {
        NONE = 0,
        RESIZE,
        LINE,
        MOVE
    }
}
