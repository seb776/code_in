using System;
using code_in.Views.NodalView.NodesElems.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using code_in.Exceptions;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public class UnSupExpNode : AExpressionNode
    {
        public TextBox NodeText;

        public UnSupExpNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView, ILinkContainer linkContainer) :
            base(themeResDict, nodalView, linkContainer)
        {
            this.SetType("Unsupported Expr");
            TextBox tb = new TextBox();
            this.NodeText = tb;
            this.ContentLayout.Children.Add(tb);
            this.SetThemeResources("");
        }
        public UnSupExpNode() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        { throw new DefaultCtorVisualException(); }

        #region INodeElem
        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.NodeText.Text = this.GetNodePresenter().GetASTNode().ToString();
        }
        #endregion INodeElem

        public override void UpdateAnchorAttachAST()
        {
            // Not necessary we have no inputs
        }
        public override void SetThemeResources(string keyPrefix)
        {
            if (NodeText != null)
            {
                this.NodeText.SetResourceReference(TextBox.ForegroundProperty, "AValueNodeSeparatorForeGroundColor");
                this.NodeText.SetResourceReference(TextBox.BackgroundProperty, "AValueNodeSecondaryColor");
            }
            else
            {
                base.SetThemeResources(keyPrefix);
            }
        }
    }
}
