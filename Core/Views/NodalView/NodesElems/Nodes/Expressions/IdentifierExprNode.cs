using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class IdentifierExprNode : AExpressionNode
    {
        public IdentifierExprNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("IdentifierExpr");
        }

        public override void InstantiateASTNode() // TODO
        {
            throw new NotImplementedException();
        }
    }
}
