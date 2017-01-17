using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class ThrowStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public override bool IsExpanded
        {
            get
            {
                return Expression.IsExpanded;
            }
            set
            {
                Expression.IsExpanded = value;
            }
        }

        public ThrowStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Throw");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public ThrowStmtTile() :
            base(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            var stmt = (this.Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.ThrowStatement);
            this.SetName("Throw");
            Expression.SetName(stmt.Expression.ToString());
        }
    }
}