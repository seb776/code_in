using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ExprStmtTile : BaseTile
    {
        public ExpressionItem Expression;

        public ExprStmtTile()
        {
            Expression = this.CreateAndAddItem<ExpressionItem>();
        }
    }
}
