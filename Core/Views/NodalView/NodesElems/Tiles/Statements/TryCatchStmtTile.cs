﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class TryCatchStmtTile : BaseTile
    {
        public FlowTileItem Statements = null;

        public TryCatchStmtTile()
        {
            Statements = this.CreateAndAddItem<FlowTileItem>();
        }
    }
}
