using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class YieldBreakStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public YieldBreakStmtTile()
        {
            Expression = this.CreateAndAddItem<ExpressionItem>();
        }
    }
}
