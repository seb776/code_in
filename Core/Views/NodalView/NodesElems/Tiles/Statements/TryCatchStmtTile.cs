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
        public FlowTileItem itemFinally = null;

        public TryCatchStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("TryCatch");
            ItemTry = this.CreateAndAddItem<FlowTileItem>();
            ItemTry.SetName("Try");
            ItemsCatch = new List<FlowTileItem>();
        }
        public TryCatchStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.TryCatchStatement);
            this.SetName("TryCatch");
            int i = 0;

            foreach (var catchItem in stmt.CatchClauses)
            {
                // set type and varibleName here
            }
        }
    }
}
