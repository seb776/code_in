using code_in.Views.NodalView.NodesElems.Tiles.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class SwitchStmtTile : BaseTile
    {
        public ExpressionItem Condition;

        public SwitchStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            Condition = this.CreateAndAddItem<ExpressionItem>(true);

        }
        public SwitchStmtTile() :
            base(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
