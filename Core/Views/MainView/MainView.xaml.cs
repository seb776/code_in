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
using System.Runtime.InteropServices;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.Utils;
using code_in.Views.NodalView.NodesElems.Items;
namespace code_in.Views.MainView
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class MainView : UserControl, stdole.IDispatch, ICodeInVisual
    {
        public NodalView.NodalView NodalV
        {
            get
            {
                return _nodalView;
            }
        }
        private NodalView.NodalView _nodalView = null;
        private int _zoomLevel = 100;
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public string _filePath { get; private set; }
        private const float _maxZoomLevel = 2.0f;
        private const float _minZoomLevel = 0.5f;
        private float _currentZoomLevel = 1.25f;
        public SearchBar SearchBar = null;

        public void OpenFile(String filePath)
        {
            this._nodalView.OpenFile(filePath);
            this._filePath = filePath;
        }
        public void EditFunction(FuncDeclItem node)
        {
            this._nodalView.EditFunction(node);
        }
        public void EditProperty(PropertyItem node, bool isGetter)
        {
            this._nodalView.EditProperty(node, isGetter);
        }
        public void EditConstructor(ConstructorItem node)
        {
            this._nodalView.EditConstructor(node);
        }
        public MainView(ResourceDictionary resourceDict)
        {
            this._themeResourceDictionary = resourceDict;
            this._languageResourceDictionary = resourceDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
            this.SearchBar = new SearchBar(this.GetThemeResourceDictionary());
            this.SearchBar.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            this.SearchBar.SetValue(WidthProperty, Double.NaN); // Width auto
            this.WinGrid.Children.Add(this.SearchBar);
            this._nodalView = new NodalView.NodalView(this._themeResourceDictionary);
            this.ZoomPanel.Child = this._nodalView;
            this.ZoomPanel.RenderTransform = new ScaleTransform();
        }
        public MainView() :
            this(Code_inApplication.MainResourceDictionary)
        {
        }
        private void SliderZoom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.ZoomPanel != null && this._nodalView != null)
            {
                // http://stackoverflow.com/questions/14729853/wpf-zooming-in-on-an-image-inside-a-scroll-viewer-and-having-the-scrollbars-a
                var middleOfScrollViewer = new Point(this.ScrollView.ActualWidth / 2.0f, this.ScrollView.ActualHeight / 2.0f);
                Point mouseAtImage = this.ScrollView.TranslatePoint(middleOfScrollViewer, this._nodalView); // ScrollViewer_CanvasMain.TranslatePoint(middleOfScrollViewer, Canvas_Main);
                Point mouseAtScrollViewer = new Point(this.ScrollView.ActualWidth / 2.0f, this.ScrollView.ActualHeight / 2.0f);// e.GetPosition(this.ScrollView);

                ScaleTransform st = this.ZoomPanel.LayoutTransform as ScaleTransform;
                if (st == null)
                {
                    st = new ScaleTransform();
                    ZoomPanel.LayoutTransform = st;
                }
                st.ScaleX = st.ScaleY = e.NewValue;
                #region [this step is critical for offset]
                ScrollView.ScrollToHorizontalOffset(0);
                ScrollView.ScrollToVerticalOffset(0);
                this.UpdateLayout();
                #endregion

                Vector offset = this._nodalView.TranslatePoint(mouseAtImage, ScrollView) - mouseAtScrollViewer; // (Vector)middleOfScrollViewer;
                ScrollView.ScrollToHorizontalOffset(offset.X);
                ScrollView.ScrollToVerticalOffset(offset.Y);
                this.UpdateLayout();
            }
        }
        private void ZoomPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                _lastMousePosFromWinGrid = e.GetPosition(this.WinGrid);
                _movingView = true;
            }
        }
        private bool _movingView = false;
        private Point _lastMousePosFromWinGrid = new Point(0, 0);
        private void ZoomPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _movingView = false;
        }
        private void ZoomPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            _movingView = false;
        }
        private void ZoomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movingView == true)
            {
                Point actualDiff = (Point)(_lastMousePosFromWinGrid - e.GetPosition(this.WinGrid));
                //MessageBox.Show((this.ZoomPanel.Child as UserControl).ActualWidth.ToString() + " " + (this.ZoomPanel.Child as UserControl).ActualHeight.ToString()); // Taille du nodalView ok
                //MessageBox.Show(this.ScrollView.ActualWidth.ToString() + " " + this.ScrollView.ActualHeight.ToString()); // Taille du scrollview ok
                //MessageBox.Show((this.ZoomPanel.ActualWidth.ToString() + " " + this.ZoomPanel.ActualHeight.ToString())); // Taille du zoomPanel ok
                //MessageBox.Show((this.WinGrid.ActualWidth.ToString() + " " + this.WinGrid.ActualHeight.ToString())); // Taille de la wingrid ok

                this.ScrollView.ScrollToHorizontalOffset(this.ScrollView.HorizontalOffset + actualDiff.X);
                this.ScrollView.ScrollToVerticalOffset(this.ScrollView.VerticalOffset + actualDiff.Y);
                _lastMousePosFromWinGrid = e.GetPosition(this.WinGrid);
            }
        }
        float _currentZoomScale = 1.0f;
        private void WinGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {

                e.Handled = true;
                if (this.ZoomPanel != null && this._nodalView != null)
                {
                    // http://stackoverflow.com/questions/14729853/wpf-zooming-in-on-an-image-inside-a-scroll-viewer-and-having-the-scrollbars-a
                    Point mouseAtImage = e.GetPosition(this._nodalView); // ScrollViewer_CanvasMain.TranslatePoint(middleOfScrollViewer, Canvas_Main);
                    Point mouseAtScrollViewer = e.GetPosition(this.ScrollView);

                    ScaleTransform st = this.ZoomPanel.LayoutTransform as ScaleTransform;
                    if (st == null)
                    {
                        st = new ScaleTransform();
                        ZoomPanel.LayoutTransform = st;
                    }

                    if (e.Delta > 0)
                    {
                        st.ScaleX = st.ScaleY = st.ScaleX * 1.25;
                        if (st.ScaleX > this.ZoomSlider.Maximum) st.ScaleX = st.ScaleY = this.ZoomSlider.Maximum;
                    }
                    else
                    {
                        st.ScaleX = st.ScaleY = st.ScaleX / 1.25;
                        if (st.ScaleX < this.ZoomSlider.Minimum) st.ScaleX = st.ScaleY = this.ZoomSlider.Minimum;
                    }
                    this.ZoomSlider.Value = st.ScaleX;
                    #region [this step is critical for offset]
                    ScrollView.ScrollToHorizontalOffset(0);
                    ScrollView.ScrollToVerticalOffset(0);
                    this.UpdateLayout();
                    #endregion

                    Vector offset = this._nodalView.TranslatePoint(mouseAtImage, ScrollView) - mouseAtScrollViewer; // (Vector)middleOfScrollViewer;
                    ScrollView.ScrollToHorizontalOffset(offset.X);
                    ScrollView.ScrollToVerticalOffset(offset.Y);
                    this.UpdateLayout();
                }

            }
        }
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }
        public void SetThemeResources(String keyPrefix)
        {

        }

        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
    }
}
