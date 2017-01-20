using code_in.Views.NodalView.NodesElems.Anchors;
using ICSharpCode.NRefactory.CSharp;
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
        public ArrayInitExprNode(ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("ArrayInitExpr");
            CanAddInputs = true;
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
        }
        public override void RemoveRuntimeParamFromAST(int index)
        {
            var astNode = this.Presenter.GetASTNode();
            var arrayInit = astNode as ICSharpCode.NRefactory.CSharp.ArrayInitializerExpression;

            arrayInit.Elements.Remove(arrayInit.Elements.ElementAt(index));
        }
        public override void _addVariableParamInAST()
        {
            var astNode = this.Presenter.GetASTNode();
            var arrayInit = astNode as ICSharpCode.NRefactory.CSharp.ArrayInitializerExpression;

            arrayInit.Elements.Add(new ICSharpCode.NRefactory.CSharp.IdentifierExpression(""));
        }
        public override void UpdateAnchorAttachAST()
        {
            var arrayInitASTNode = this.Presenter.GetASTNode() as ArrayInitializerExpression;

            int iChildren = 0;
            foreach (var v in this._inputs.Children)
            {

                if ((v is DataFlowAnchor))
                {
                    arrayInitASTNode.Elements.ReplaceWith(arrayInitASTNode.Elements);
                    if (iChildren < arrayInitASTNode.Elements.Count)
                    {
                        var expr = arrayInitASTNode.Elements.ElementAt(iChildren);

                        (v as DataFlowAnchor).SetASTNodeReference((e) =>
                        {
                            arrayInitASTNode.Elements.InsertAfter(expr, e);
                            arrayInitASTNode.Elements.Remove(expr);
                        });
                    }
                    ++iChildren;
                }
            }
        }
    }
}
