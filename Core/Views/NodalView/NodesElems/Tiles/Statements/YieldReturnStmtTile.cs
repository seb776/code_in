using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class YieldReturnStmtTile : BaseTile
    {
        public ExpressionItem Expression = null;

        public YieldReturnStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("Yield Return");
            Expression = this.CreateAndAddItem<ExpressionItem>(true);
        }
        public YieldReturnStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
