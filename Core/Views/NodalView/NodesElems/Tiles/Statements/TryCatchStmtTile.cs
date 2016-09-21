using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class TryCatchStmtTile : BaseTile
    {
        public FlowTileItem Statements = null;

        public TryCatchStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            Statements = this.CreateAndAddItem<FlowTileItem>();

        }
        public TryCatchStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
