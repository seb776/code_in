using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class TypeOfExprNode : AExpressionNode
    {
        public DataFlowAnchor Input = null;
        public TypeOfExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Sizeof");
            Input = this.CreateAndAddInput<DataFlowAnchor>();
        }
        public override void UpdateAnchorAttachAST()
        {}
    }
}
