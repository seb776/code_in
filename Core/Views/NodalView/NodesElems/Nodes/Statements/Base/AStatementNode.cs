using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Base
{
    public abstract class AStatementNode : AIONode
    {
        public AStatementNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("Statement");
            this.SetDynamicResources("DefaultStmtNode");
        }

        #region ICodeInVisual
        public override void SetDynamicResources(String keyPrefix)
        {
            this.NodeName.SetResourceReference(ForegroundProperty, keyPrefix + "NameForeGroundColor");
            this.NodeType.SetResourceReference(ForegroundProperty, keyPrefix + "TypeForeGroundColor");
            this.NodeHeader.SetResourceReference(BackgroundProperty, keyPrefix + "SecondaryColor");
            this.NodeBorder.SetResourceReference(BorderBrushProperty, keyPrefix + "SecondaryColor");
            this.BackGrid.SetResourceReference(BackgroundProperty, keyPrefix + "MainColor");
        }
        #endregion ICodeInVisual
    }
}
