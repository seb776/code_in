using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class DoWhileStmtTile : BaseTile
    {
        public FlowTileItem Condition = null;
        public FlowTileItem trueItem = null;

        public DoWhileStmtTile()
        {
            Condition = new FlowTileItem();
            trueItem = new FlowTileItem();
            Condition.ItemName.Content = "Condition";
            trueItem.ItemName.Content = "True";
            this.TileContent.Children.Add(Condition);
            this.TileContent.Children.Add(Condition);
        }
    }
}
