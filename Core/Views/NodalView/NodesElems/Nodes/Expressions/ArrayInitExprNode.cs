using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class ArrayInitExprNode : AExpressionNode
    {
        public ArrayInitExprNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("ArrayInitExpr");
        }

        public override void InstantiateASTNode()
        {
            throw new NotImplementedException();
        }
    }
}
