﻿using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForEachStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public ForEachStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("Foreach");
            Condition = this.CreateAndAddItem<ExpressionItem>(true);
            trueItem = this.CreateAndAddItem<FlowTileItem>();
            trueItem.SetName("Statements");
        }
        public ForEachStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
