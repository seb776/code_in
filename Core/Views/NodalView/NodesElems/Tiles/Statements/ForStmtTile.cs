using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;
        public override bool IsExpanded
        {
            get
            {
                return Condition.IsExpanded;
            }
            set
            {
                Condition.IsExpanded = value;
                trueItem.IsExpanded = value;
            }
        }
        public ForStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            trueItem = this.CreateAndAddItem<FlowTileItem>();
            this.SetName("For");
            this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(51, 0x20, 0x77, 0xE3));
        }
        public ForStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("For");
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ForStatement);
            Condition.SetName(stmt.Condition.ToString());
        }
    }
}
