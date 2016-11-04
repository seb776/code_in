using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems
{
    public enum EDragMode
    {
        MOVEOUT,
        STAYINCONTEXT
    }
    public interface IVisualNodeContainerDragNDrop : IVisualNodeContainer
    {
        void AddSelectNode(IDragNDropItem item);
        void AddSelectNodes(List<IDragNDropItem> items);
        void Drag(EDragMode dragMode);
        void UpdateDragInfos();
        void Drop(List<IDragNDropItem> items);

        //bool IsDropValid();
        //int GetDropNodeIndex(Point pos); // Gets the index where the element will be pushed
        //void HighLightDropNodePlace(Point pos); // Displays a visual element to show where the node will be dropped
    }
}
