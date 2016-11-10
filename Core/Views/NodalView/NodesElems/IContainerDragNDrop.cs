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
        NONE,
        MOVEOUT,
        STAYINCONTEXT
    }
    public interface IContainerDragNDrop
    {
        //void AddSelectNode(IDragNDropItem item);
        //void AddSelectNodes(List<IDragNDropItem> items);
        //void UnselectNode(IDragNDropItem item);
        //void UnselectAllNodes();
        void Drag(EDragMode dragMode);
        void UpdateDragInfos(Point mousePosToMainGrid);
        void Drop(IEnumerable<IDragNDropItem> items);
        bool IsDropValid(IEnumerable<IDragNDropItem> items);

        //bool IsDropValid();
        //int GetDropNodeIndex(Point pos); // Gets the index where the element will be pushed
        //void HighLightDropNodePlace(Point pos); // Displays a visual element to show where the node will be dropped
    }
}
