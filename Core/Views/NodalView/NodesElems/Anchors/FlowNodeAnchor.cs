using code_in.Exceptions;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Anchors
{
    public class FlowNodeAnchor : AIOAnchor
    {
        public void AttachASTStmt(FlowNodeAnchor rightNode)
        {
            // TODO @Seb To remove
            //var stmtToInsert = (rightNode.ParentNode as AStatementNode).GetNodePresenter().GetASTNode() as Statement;
            ////MessageBox.Show("AttachASTStmt " + (MethodAttachASTStmt != null).ToString() + " " + this.ParentNode.GetType().ToString());
            //if (MethodAttachASTStmt != null)
            //    this.MethodAttachASTStmt(stmtToInsert);
        }
        public Func<Statement, Statement> MethodAttachASTStmt = null;
        public Action MethodDetachASTStmt = null;

         public FlowNodeAnchor(ResourceDictionary themeResDict, ILinkContainer linkContainer) :
            base(themeResDict, linkContainer)
        {
            //this.SetName("FlowNode");
        }
         public FlowNodeAnchor() :
             this(Code_inApplication.MainResourceDictionary, null)
         { throw new DefaultCtorVisualException(); }
    }
}
