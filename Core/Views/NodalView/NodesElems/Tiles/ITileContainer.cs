using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    public interface ITileContainer
    {
        T CreateAndAddTile<T>() where T : ITile;
        void AddTile<T>(T tile, int index = -1) where T : ITile;
        void RemoveTile(ITile tile);
    }
}
