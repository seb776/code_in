using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class GotoStmtTile : BaseTile
    {

        public override bool IsExpanded
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public GotoStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Goto");
        }
        public GotoStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.GotoStatement);
            this.SetName(stmt.ToString());
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }
    }
}