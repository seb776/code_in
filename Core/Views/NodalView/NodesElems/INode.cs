using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    public interface INode
    {
        void SetName(String name);
        String GetName();
        void SetDynamicResources(String keyPrefix);
        NodalView GetNodalView();
        BaseNode GetParentNode();
        void SetNodalView(NodalView nv);
        void SetParentNode(BaseNode parent);
    }

    public enum TransformationMode
    {
        NONE = 0,
        RESIZE = 1,
        MOVE = 2,
        LINE = 3,
        MOVEORDERED = 4
    }
    /*public static Point begin;
    public static Line lineInput;
    public static Line lineOutput;
    public static int orientationStart = 0; // 0 : none, 1 : input, 2 : output
    */
}
