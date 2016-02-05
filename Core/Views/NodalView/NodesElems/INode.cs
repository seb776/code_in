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

    public static class TransformingNode
    {
        public static Object TransformingObject = null; // Used to move, resize or link nodes
        public enum TransformationMode
        {
            NONE = 0,
            RESIZE = 1,
            MOVE = 2,
            LINE = 3
        }
        public static TransformationMode Transformation;

        /*public static Point begin;
        public static Line lineInput;
        public static Line lineOutput;
        public static int orientationStart = 0; // 0 : none, 1 : input, 2 : output
        */
    }
}
