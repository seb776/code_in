using code_in.Views.NodalView.NodesElem.Nodes.Base;
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

namespace code_in.Views.NodalView.NodesElems.Anchors
{
    public class IOLink
    {
        public IOLink()
        {
        }
        public Code_inLink Link = null;
        public AIOAnchor Input = null;
        public AIOAnchor Output = null; // The node where the link starts
        public void CheckAssert()
        {
            System.Diagnostics.Debug.Assert(Link != null && Input != null && Output != null && Input != Output);
        }
    }

    /// <summary>
    /// Logique d'interaction pour AIOAnchor.xaml
    /// </summary>
    public partial class AIOAnchor : UserControl, ICodeInVisual, ICodeInTextLanguage
    {
        public AIONode ParentNode { get; private set; }
        public void UpdateLinksPosition()
        {
            Point halfRectAnchorSize = new Point(this.LinkAttach.Width / 2.0, this.LinkAttach.Height / 2.0);
            Point anchorPos = this.LinkAttach.TranslatePoint(halfRectAnchorSize, (this._parentLinksContainer as UIElement));
            foreach (var l in _links)
            {
                IOLink link = l as IOLink;
                if (this.Orientation == EOrientation.LEFT)
                {
                    link.Link._x2 = anchorPos.X;
                    link.Link._y2 = anchorPos.Y;
                }
                else if (this.Orientation == EOrientation.RIGHT)
                {
                    link.Link._x1 = anchorPos.X;
                    link.Link._y1 = anchorPos.Y;
                }
                link.Link.InvalidateVisual();
            }
        }
        public List<IOLink> _links = null;
        public void AttachNewLink(IOLink link)
        {
            System.Diagnostics.Debug.Assert(link != null);
            link.CheckAssert();
            _links.Add(link);
        }
        public void RemoveLink(IOLink link, bool detachAST)
        {
            if (link.Input is DataFlowAnchor)
                (link.Input as DataFlowAnchor).MethodAttachASTExpr(new ICSharpCode.NRefactory.CSharp.NullReferenceExpression());
            else if (link.Output is FlowNodeAnchor && (link.Output as FlowNodeAnchor).MethodDetachASTStmt != null && detachAST)
                (link.Output as FlowNodeAnchor).MethodDetachASTStmt();
            _links.Remove(link);
        }
        public Point GetAnchorPosition(UIElement relative)
        {
            Point attachLinkHelfSize = new Point(this.LinkAttach.Width / 2.0, this.LinkAttach.Height / 2.0);
            return this.LinkAttach.TranslatePoint(attachLinkHelfSize, relative);
        }
        public enum EOrientation
        {
            LEFT = 0,
            RIGHT = 1,
        }
        private ResourceDictionary _themeResourceDictionary = null;
        private EOrientation _orientation = EOrientation.LEFT;
        private ILinkContainer _parentLinksContainer = null;
        public ILinkContainer ParentLinksContainer
        {
            get
            {
                return _parentLinksContainer;
            }
        }

        public void SetParentLinksContainer(ILinkContainer linksContainer)
        {
            //System.Diagnostics.Debug.Assert(linksContainer != null);
            _parentLinksContainer = linksContainer;
        }

        public void SetParentNode(AIONode parentNode)
        {
            System.Diagnostics.Debug.Assert(parentNode != null);
            ParentNode = parentNode;
        }

        public EOrientation Orientation
        {
            get { return _orientation; }
            set
            {
                this.FlowDirection = (value == EOrientation.LEFT ?
                    System.Windows.FlowDirection.LeftToRight :
                    System.Windows.FlowDirection.RightToLeft);

                this.HorizontalAlignment = (value == EOrientation.LEFT ? HorizontalAlignment.Left : HorizontalAlignment.Right);
                _orientation = value;
            }
        }

        public AIOAnchor(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            this._links = new List<IOLink>();
            this.MouseLeftButtonDown += AIOAnchor_MouseLeftButtonDown;
            this.MouseLeftButtonUp += AIOAnchor_MouseLeftButtonUp;
        }

        void AIOAnchor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _parentLinksContainer.DropLink(this, false);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
            }
            e.Handled = true;
        }
        public void DebugDisplay()
        {
            this.LinkAttach.Fill = new SolidColorBrush(Colors.Red);
        }

        void AIOAnchor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _parentLinksContainer.DragLink(this, false);
        }

        #region This
        public void SetName(String name)
        {
            this.IOTextContent.Content = name;
        }
        public String GetName() { return this.IOTextContent.Content.ToString(); }
        #endregion This
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
        #region ICodeInTextLanguage
        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInTextLanguage
    }
}
