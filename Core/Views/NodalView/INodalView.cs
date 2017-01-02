﻿using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView
{
    public interface INodalView : IVisualNodeContainer, IContainerDragNDrop
    {
        //void RemoveLink(AIOAnchor anchor);

        ITileContainer RootTileContainer
        {
            get;
            set;
        }
        void AlignDeclarations();
        Dictionary<string, List<INodeElem>> SearchMatchinNodes(string name, bool[] userOptions);
    }
}
