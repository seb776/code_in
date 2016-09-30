using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    class IfStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem ItemTrue = null;
        public FlowTileItem ItemFalse = null;

        public IfStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("If");
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            ItemTrue = this.CreateAndAddItem<FlowTileItem>();
            ItemTrue.SetName("true");
            ItemFalse = this.CreateAndAddItem<FlowTileItem>();
            ItemFalse.SetName("false");
            ItemFalse.SetThemeResources("false");
        }
        public IfStmtTile()  :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(this._presenter != null);
            var ifElse = (this._presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.IfElseStatement);
            Condition.SetName(ifElse.Condition.ToString());
            //ItemTrue.SetName(ifElse.TrueStatement.ToString());
            //ItemFalse.SetName(ifElse.FalseStatement.ToString());
        }
    }
}
