using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ExprStmtTile : BaseTile
    {
        public FlowTileItem Expression;

        public ExprStmtTile()
        {
            Expression = new FlowTileItem();
            this.TileContent.Children.Add(Expression);
        }
    }
}
