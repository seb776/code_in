using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.MainView.Nodes.Items
{
    /// <summary>
    /// Interaction logic for NodeItem.xaml
    /// </summary>
    public partial class NodeItem : UserControl, INodeItem
    {
        protected BaseNode _parentNode;
        public BaseNode ParentNode
        {
            get
            {
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }
        public NodeItem()
        {
            InitializeComponent();
            _parentNode = null;
        }

        public NodeItem(BaseNode parent) :
            this()
        {
            System.Diagnostics.Debug.Assert(parent != null, "The parentNode is null");
            _parentNode = parent;
        }

        // TODO: Temporary may be replaced by a binding
        public void SetName(String n)
        {
            this.ItemName.Text = n;
        }

        public enum EOrientation
        {
            LEFT = 0,
            RIGHT = 1,
        }
        public void SetItemType(String type)
        {
            if (this.Container.FindName("TypeField") == null)
            {
                TypeInfo ti = new TypeInfo();

                ti.Name = "TypeField"; // Not necessary but to be clean ;)
                this.Container.RegisterName(ti.Name, ti); // To allow TypeField to be found through FindName
                this.Container.Children.Add(ti);
            }
            object typeField = this.FindName("TypeField");
            if (typeField != null)
            {
                var tF = typeField as TypeInfo;
                tF.Content = type;
            }
        }

        
        private EOrientation _orientation;
        public EOrientation Orientation
        {
            get { return _orientation; }
            set
            {
                this.Container.FlowDirection = (value == EOrientation.LEFT ?
                    System.Windows.FlowDirection.LeftToRight :
                    System.Windows.FlowDirection.RightToLeft);
                
                TypeInfo ti = (TypeInfo)this.Container.FindName("TypeField");
                if (this.Container.FindName("TypeField") != null) // TypeField is not always part of an item
                    ti.TypePanel.FlowDirection = System.Windows.FlowDirection.LeftToRight; // to avoid the type visual control to be reversed recursively

                _orientation = value;
            }
        }
    }
}
