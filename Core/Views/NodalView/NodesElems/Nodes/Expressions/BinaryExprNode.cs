﻿using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class BinaryExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA = null;
        public DataFlowAnchor OperandB = null;
        public BinaryExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
            OperandB = this.CreateAndAddInput<DataFlowAnchor>();
            OperandA.SetName("A");
            OperandB.SetName("B");
            this.SetType("BinaryExpr");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var binaryOpExpr = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression;
            this.SetType(binaryOpExpr.OperatorToken.ToString());
        }

        public override void UpdateAnchorAttachAST()
        {
            OperandA.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression).Left = e; });
            OperandB.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression).Right = e; });
        }
    }

}
