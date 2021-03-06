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
    public class TernaryExprNode : AExpressionNode
    {
        public DataFlowAnchor OperandA = null;
        public DataFlowAnchor OperandB = null;
        public DataFlowAnchor OperandC = null;
        public TernaryExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            OperandA = this.CreateAndAddInput<DataFlowAnchor>();
            OperandB = this.CreateAndAddInput<DataFlowAnchor>();
            OperandC = this.CreateAndAddInput<DataFlowAnchor>();
            this.SetName("Ternary");
            OperandA.SetName("Condition");
            OperandB.SetName("B");
            OperandC.SetName("C");
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            // Not Needed
        }

        public override void UpdateAnchorAttachAST()
        {
            OperandA.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ConditionalExpression).Condition = e; });
            OperandB.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ConditionalExpression).TrueExpression = e; });
            OperandC.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ConditionalExpression).FalseExpression = e; });
        }
    }
}
