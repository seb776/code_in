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

        public ForEachStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Foreach");
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            trueItem = this.CreateAndAddItem<FlowTileItem>();
            trueItem.SetName("");
            trueItem.DescriptionPanel.Background = new SolidColorBrush(Colors.Transparent);
            this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(51, 0x20, 0x77, 0xE3));
        }
        public ForEachStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(Presenter != null);
            var foreachStmt = (Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ForeachStatement);
            this.Condition.SetName(foreachStmt.InExpression.ToString());
            
        }
    }
}
