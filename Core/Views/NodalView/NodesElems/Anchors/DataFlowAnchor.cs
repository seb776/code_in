using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Anchors
{
    public class DataFlowAnchor : AIOAnchor
    {

        public DataFlowAnchor(ResourceDictionary themeResDict, ILinkContainer linkContainer) :
            base(themeResDict, linkContainer)
        {
        }
        public Action<ICSharpCode.NRefactory.CSharp.Expression> MethodAttachASTExpr = null;
        public void SetASTNodeReference(Action<ICSharpCode.NRefactory.CSharp.Expression> methodAttach)
        {
            MethodAttachASTExpr = methodAttach;
        }
    }
}
