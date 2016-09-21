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
        public YieldBreakStmtTile(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetName("Yield Break");
        }
        public YieldBreakStmtTile() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
    }
}
