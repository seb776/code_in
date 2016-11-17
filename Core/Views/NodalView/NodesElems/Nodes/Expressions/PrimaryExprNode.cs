using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class PrimaryExprNode : AExpressionNode
    {
        public PrimaryExprNode(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetType("PrimaryExpr");
        }

        public override void InstantiateASTNode() // TODO
        {
            throw new NotImplementedException();
        }

        //public override void UpdateDisplayedInfosFromPresenter()
        //{
        //    var nodePres = GetNodePresenter();
        //    var astNode = nodePres.GetASTNode() as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
        //    ExprOut.SetName(astNode.LiteralValue);
        //}
    }
}
