using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class LabelStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public LabelStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Label");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public LabelStmtTile() :
            this(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("Label");
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.LabelStatement);
            Expression.SetName(stmt.Label);
        }
    }
}
