using code_in.Presenters.Nodal;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Managers
{
    /// <summary>
    /// This class is used to manage Copy/Cut/Paste, and inter-files moves.
    /// </summary>
    public class RootDragNDropManager
    {
        public ILinkContainer ParentLinkContainer;
        public bool DraggingLink;
        public bool StartedMove
        {
            get;
            private set;
        }
        public HashSet<IDragNDropItem> SelectedItems
        {
            get;
            private set;
        }
        public HashSet<IDragNDropItem> copyItems
        {
            get;
            private set;
        }
        public EDragMode DragMode
        {
            get;
            private set;
        }

        public RootDragNDropManager()
        {
            SelectedItems = new HashSet<IDragNDropItem>();
            copyItems = null;
            DragMode = EDragMode.NONE;
            StartedMove = false;
            DraggingLink = false;
            ParentLinkContainer = null;
        }
        public void AddSelectItem(IDragNDropItem item)
        {
            if (SelectedItems.Count != 0 && SelectedItems.ElementAt(0).GetParentView() != item.GetParentView())
            {
                UnselectAllNodes();
            }
            _selectNode(item);
        }
        private void _selectNode(IDragNDropItem item)
        {
            item.SelectHighLight(true);
            SelectedItems.Add(item);
        }
        public void UnselectAllNodes()
        {
            foreach (var s in SelectedItems)
                s.SelectHighLight(false);
            SelectedItems.Clear();
        }
        public bool IsSelectedItem(IDragNDropItem item)
        {
            Debug.Assert(item != null);
            return SelectedItems.Contains(item);
        }
        public void UpdateDragInfos(EDragMode dragMode, System.Windows.Point mousePosToMainGrid)
        {
            if (DraggingLink)
            {

            }
            else if (SelectedItems.Count > 0)
            {
                if (!StartedMove)
                {
                    _drag(dragMode);
                    StartedMove = true;
                }

                SelectedItems.ElementAt(0).GetParentView().UpdateDragInfos(mousePosToMainGrid);
            }
        }
        private void _drag(EDragMode dragMode)
        {
            if (SelectedItems.Count != 0)
            {
                DragMode = dragMode;
                SelectedItems.ElementAt(0).GetParentView().Drag(dragMode);
            }
        }
        public void Drop(IContainerDragNDrop parentContainer)
        {
            if (DraggingLink)
            {
                Debug.Assert(ParentLinkContainer != null);
                ParentLinkContainer.DropLink(null, false);// TODO @Seb params~
            }
            else
            {
                IContainerDragNDrop container = parentContainer;
                if (DragMode == EDragMode.STAYINCONTEXT && SelectedItems.Count > 0)
                    container = SelectedItems.ElementAt(0).GetParentView();
                if (container != null)
                {
                    if (container.IsDropValid(SelectedItems))
                        container.Drop(SelectedItems);
                }
                DragMode = EDragMode.NONE;
                System.Windows.Input.Mouse.OverrideCursor = null;
                StartedMove = false;
            }
        }

        public void duplicateNode()
        {
            if (copyItems == null) {
                copyItems = new HashSet<IDragNDropItem>(SelectedItems);
            }
            else
            {
                foreach (var node in SelectedItems)
                {

                }
            }

        }

        //void SelectNode(INodeElem node);
        //void UnSelectNode(INodeElem node);
        //void RevertChange(); // Resets nodes to their original positions. To be used when a DragNDrop is not valid.
        //void UnSelectAllNodes();
        //void DragNodes();
        //void DropNodes(IContainerDragNDrop container);
        //void UpdateDragState(Point pos); // This function is here to update the view when mouse is moving (update view and links)
    }
}
