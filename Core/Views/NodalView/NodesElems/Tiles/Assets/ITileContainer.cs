using code_in.Presenters.Nodal.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    public interface ITileContainer
    {
        T CreateAndAddTile<T>(INodePresenter presenter) where T : BaseTile; // TODO @Seb add NodePresenter
        void AddTile<T>(T tile, int index = -1) where T : BaseTile;
        void RemoveTile(BaseTile tile);
        bool IsExpanded
        {
            get;
            set;
        }
        void UpdateDisplayedInfosFromPresenter(); // cal the same function for each underlying statement
    }
}
