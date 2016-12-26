using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class fixedStmtTile : BaseTile
    {
        public FlowTileItem itemsFixed = null;

        public fixedStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("fixed");
            itemsFixed = this.CreateAndAddItem<FlowTileItem>();
            itemsFixed.SetName("fixed");
        }
        public fixedStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.FixedStatement);
        }
    }
}
