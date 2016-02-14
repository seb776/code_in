using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    public interface INodeElem
    {
        void SetName(String name);
        String GetName();
        void SetParentView(INodeElem vc); // Each node elem is inside a masterView
        INodeElem GetParentView();
        void SetRootView(IVisualNodeContainerDragNDrop dnd);
        void RemoveNode(INodeElem node);
        IVisualNodeContainerDragNDrop GetRootView();
    }
    /*public static Point begin;
    public static Line lineInput;
    public static Line lineOutput;
    public static int orientationStart = 0; // 0 : none, 1 : input, 2 : output
    */
}
