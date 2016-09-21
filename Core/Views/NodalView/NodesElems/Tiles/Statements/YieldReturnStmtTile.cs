using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class YieldReturnStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public YieldReturnStmtTile()
        {
            Expression = this.CreateAndAddItem<ExpressionItem>();
        }
    }
}
