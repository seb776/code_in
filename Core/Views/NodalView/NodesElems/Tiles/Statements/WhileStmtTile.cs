using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class WhileStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public WhileStmtTile()
        {
            Condition = this.CreateAndAddItem<ExpressionItem>();
            trueItem = this.CreateAndAddItem<FlowTileItem>();
        }
    }
}
