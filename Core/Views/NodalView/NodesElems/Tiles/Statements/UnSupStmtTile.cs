using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class UnSupStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;
        public UnSupStmtTile(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            this.SetName("Unsupported");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public UnSupStmtTile() :
            base(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        #region INodeElem
        public override void UpdateDisplayedInfosFromPresenter()
        {
            Debug.Assert(this._presenter != null);
            var unSup = this._presenter.GetASTNode();
            Expression.SetName(unSup.ToString());
        }
        #endregion INodeElem
    }
}
