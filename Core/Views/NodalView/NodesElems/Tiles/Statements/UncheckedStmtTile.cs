using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class UncheckedStmtTile : BaseTile
    {
        public FlowTileItem itemsUnchecked = null;

        public UncheckedStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("unchecked");
            itemsUnchecked = this.CreateAndAddItem<FlowTileItem>();
            itemsUnchecked.SetName("unchecked");
        }
        public UncheckedStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.UncheckedStatement);
        }
    }
}
