using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class NamespaceNode : AFlyingContentNode
    {
        public NamespaceNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView, INodePresenter nodePresenter) :
            base(themeResDict, nodalView)
        {
            this.Presenter = nodePresenter;
            this.SetType("namespace");
            this.SetName("TMP.DefaultNamespaceName");
            //this.SetThemeResources("NamespaceNode");
        }
        public NamespaceNode()
            : this(Code_inApplication.MainResourceDictionary, null, null)
        { throw new DefaultCtorVisualException(); }

        #region IVisualNodeContainer
        public override void HighLightDropPlace(System.Windows.Point pos)
        {
            throw new NotImplementedException();
        }
        
        public override int GetDropIndex(System.Windows.Point pos)
        { 
            return 0; 
        }

        public override void RemoveNode(INodeElem node)
        {
            this.ContentGridLayout.Children.Remove(node as UIElement);
        }
        #endregion IVisualNodeContainer

        public override void AddCreatedNodeToAST(ICSharpCode.NRefactory.CSharp.AstNode node)
        {
            var thisTypeAst = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.NamespaceDeclaration;
            thisTypeAst.AddMember(thisTypeAst);
        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            var astNode = this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.NamespaceDeclaration;
            this.SetName(astNode.Name);
        }
    } // Class
} // Namespace
