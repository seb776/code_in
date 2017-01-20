using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class FuncCallExprNode : AExpressionNode
    {
        public DataFlowAnchor TargetIn = null; // The method name
        public FuncCallExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer)
            : base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Method call");
            TargetIn = this.CreateAndAddInput<DataFlowAnchor>();
            TargetIn.SetName("TMP: MethodName");
            CanAddInputs = true;
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            if (!(this.Presenter.GetASTNode() is ICSharpCode.NRefactory.CSharp.InvocationExpression)) // TODO remove (quickfix)
                return;
            var target = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.InvocationExpression).Target;
            if (target != null)
                this.SetName(target.ToString() + "()");
        }
        public override void _addVariableParamInAST()
        {
            var astNode = this.Presenter.GetASTNode();
            var invokExpr = astNode as ICSharpCode.NRefactory.CSharp.InvocationExpression;

            invokExpr.Arguments.Add(new ICSharpCode.NRefactory.CSharp.IdentifierExpression(""));
        }
        public override void RemoveRuntimeParamFromAST(int index)
        {
            var astNode = this.Presenter.GetASTNode();
            var invokExpr = astNode as ICSharpCode.NRefactory.CSharp.InvocationExpression;

            invokExpr.Arguments.Remove(invokExpr.Arguments.ElementAt(index - 1));
        }
        public override void UpdateAnchorAttachAST()
        {
            var astNode = this.Presenter.GetASTNode();

            if (!(astNode is ICSharpCode.NRefactory.CSharp.InvocationExpression))
                throw new NotImplementedException();
            var invokExpr =  astNode as ICSharpCode.NRefactory.CSharp.InvocationExpression;
            TargetIn.SetASTNodeReference((e) => { (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.InvocationExpression).Target = e; });


            int iChildren = 0;
            bool first = true;
            foreach (var v in this._inputs.Children)
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if ((v is DataFlowAnchor))
                {
                    if (iChildren < invokExpr.Arguments.Count)
                    {
                        var expr = invokExpr.Arguments.ElementAt(iChildren);

                        (v as DataFlowAnchor).SetASTNodeReference((e) =>
                        {
                            invokExpr.Arguments.InsertAfter(expr, e);
                            invokExpr.Arguments.Remove(expr);
                            //expr.ReplaceWith(e);
                        });
                    }
                    ++iChildren;
                }
            }
        }
    }
}
