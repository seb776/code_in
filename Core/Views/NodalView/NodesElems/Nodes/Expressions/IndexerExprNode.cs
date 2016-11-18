using code_in.Exceptions;
using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class IndexerExprNode : AExpressionNode
    {
        public DataFlowAnchor Target
        {
            get;
            private set;
        }

        public IndexerExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            Target = this.CreateAndAddInput<DataFlowAnchor>();
        }

        public IndexerExprNode() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        { throw new DefaultCtorVisualException(); }
    }
}
