using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ForStmtTile : BaseTile
    {
        public ExpressionItem Condition = null;
        public FlowTileItem trueItem = null;

        public ForStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            Condition = this.CreateAndAddItem<ExpressionItem>();
            trueItem = this.CreateAndAddItem<FlowTileItem>();
        }
        public ForStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

    }
}
