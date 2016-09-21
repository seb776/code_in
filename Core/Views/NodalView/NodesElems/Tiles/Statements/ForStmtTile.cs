﻿using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public ForStmtTile()
        {
            Condition = this.CreateAndAddItem<ExpressionItem>();
            trueItem = this.CreateAndAddItem<FlowTileItem>();
        }
    }
}
