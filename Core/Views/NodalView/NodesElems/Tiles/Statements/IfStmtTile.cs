using code_in.Views.NodalView.NodesElems.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    class IfStmtTile : BaseTile
    {
        public BaseTile IfTile = null;
        public FlowTileItem ItemTrue = null;
        public FlowTileItem ItemFalse = null;

        public IfStmtTile() 
        {
            ItemTrue = new FlowTileItem();
            ItemTrue.ItemName.Content = "True Item";
            ItemFalse = new FlowTileItem();
            ItemFalse.ItemName.Content = "False Item";
            this.TileContent.Children.Add(ItemTrue);
            this.TileContent.Children.Add(ItemFalse);
        }
    }
}
