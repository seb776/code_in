﻿using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems
{
    public interface ILinkContainer
    {
        void DragLink(AIOAnchor from);
        void DropLink(AIOAnchor to);
        void UpdateLinkDraw(Point mousePosRelToParentLinkContainer); // This function is here to update the view when mouse is moving (update view and links)
    }
}
