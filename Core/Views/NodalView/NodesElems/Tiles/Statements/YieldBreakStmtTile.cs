using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class YieldBreakStmtTile : BaseTile
    {
        public YieldBreakStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("YieldBreak");
        }
        public YieldBreakStmtTile() :
            this(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.YieldBreakStatement;
            this.SetName("YieldBreak");

        }
    }
}
