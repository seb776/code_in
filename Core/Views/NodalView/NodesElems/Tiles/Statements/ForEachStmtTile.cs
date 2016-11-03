using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForEachStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public ForEachStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("Foreach");
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            trueItem = this.CreateAndAddItem<FlowTileItem>();
            trueItem.SetName("");
            trueItem.DescriptionPanel.Background = new SolidColorBrush(Colors.Transparent);
            this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(51, 0x20, 0x77, 0xE3));
        }
        public ForEachStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(this._presenter != null);
            var foreachStmt = (this._presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ForeachStatement);
            this.Condition.SetName(foreachStmt.InExpression.ToString());
            
        }
    }
}
