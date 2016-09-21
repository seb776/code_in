using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    class IfStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem ItemTrue = null;
        public FlowTileItem ItemFalse = null;

        public IfStmtTile() 
        {
            Condition = this.CreateAndAddItem<ExpressionItem>();
            ItemTrue = this.CreateAndAddItem<FlowTileItem>();
            ItemFalse = this.CreateAndAddItem<FlowTileItem>();
        }
    }
}
