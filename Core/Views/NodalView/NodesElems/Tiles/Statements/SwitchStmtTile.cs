using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class SwitchStmtTile : BaseTile
    {
        public ExpressionItem Condition;

        public SwitchStmtTile()
        {
            Condition = this.CreateAndAddItem<ExpressionItem>();
        }
    }
}
