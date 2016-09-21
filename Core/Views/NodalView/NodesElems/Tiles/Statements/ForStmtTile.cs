using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForStmtTile : BaseTile
    {
        public FlowTileItem Condition = null;
        public FlowTileItem trueItem = null;

        public ForStmtTile()
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
