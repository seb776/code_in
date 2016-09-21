using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ReturnStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;
        public ReturnStmtTile() 
        {
            Expression = this.CreateAndAddItem<ExpressionItem>();
        }
    }
}
