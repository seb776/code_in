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
        public FlowTileItem ItemTry = null;
        public List<FlowTileItem> ItemsCatch = null;

        public TryCatchStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("tryCatch");
            ItemTry = this.CreateAndAddItem<FlowTileItem>();
            ItemTry.SetName("try");
            ItemsCatch = new List<FlowTileItem>();
        }
        public TryCatchStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
