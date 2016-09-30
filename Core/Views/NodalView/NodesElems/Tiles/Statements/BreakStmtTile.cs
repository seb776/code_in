using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Tiles.Statements
{
    class BreakStmtTile : BaseTile
    {
        public BreakStmtTile(ResourceDictionary themeResDict) :
             base(themeResDict)
        {
            //this.SetName("Break");
        }
        public BreakStmtTile() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName("break;");
        }
    }
}
