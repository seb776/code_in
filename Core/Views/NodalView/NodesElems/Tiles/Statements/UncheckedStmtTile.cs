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
        public override bool IsExpanded
        {
            get
            {
                return itemsUnchecked.IsExpanded;
            }
            set
            {
                itemsUnchecked.IsExpanded = value;
            }
        }

        public UncheckedStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Unchecked");
            itemsUnchecked = this.CreateAndAddItem<FlowTileItem>();
        }
        public UncheckedStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.UncheckedStatement);
            this.SetName("Unchecked");
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }
    }
}
