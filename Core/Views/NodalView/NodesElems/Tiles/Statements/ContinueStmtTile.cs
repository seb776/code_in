using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ContinueStmtTile : BaseTile
    {
        public override bool IsExpanded
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public ContinueStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Continue");
        }

        public ContinueStmtTile() :
            this(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("Continue");
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ContinueStatement);
        }

        public override void UpdateAnchorAttachAST()
        {
            // useless here
        }
    }
}
