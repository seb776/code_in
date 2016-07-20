using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
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
            var stmtToInsert = (rightNode.ParentNode as AStatementNode).GetNodePresenter().GetASTNode() as Statement;
            //MessageBox.Show("AttachASTStmt " + (MethodAttachASTStmt != null).ToString() + " " + this.ParentNode.GetType().ToString());
            if (MethodAttachASTStmt != null)
                this.MethodAttachASTStmt(stmtToInsert);
        }
        public Func<Statement, Statement> MethodAttachASTStmt = null;
        public Action MethodDetachASTStmt = null;

         public FlowNodeAnchor(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            //this.SetName("FlowNode");
        }
         public FlowNodeAnchor() :
             this(Code_inApplication.MainResourceDictionary)
         { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }
    }
}
