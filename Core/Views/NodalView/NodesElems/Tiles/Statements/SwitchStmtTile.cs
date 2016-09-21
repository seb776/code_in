using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class SwitchStmtTile : BaseTile
    {
        public FlowTileItem Condition;

        public SwitchStmtTile()
        {
            Condition = new FlowTileItem();
            Condition.ItemName.Content = "condition";
            this.TileContent.Children.Add(Condition);
        }
    }
}
