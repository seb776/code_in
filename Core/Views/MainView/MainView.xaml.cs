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
        public string _filePath{get; private set;}
        private const float  _maxZoomLevel = 2.0f;
        private const float _minZoomLevel = 0.5f;
        private float _currentZoomLevel = 1.25f;

        public void OpenFile(String filePath)
        {
            this._nodalView.OpenFile(filePath);
            this._filePath = filePath;
        }

        public void EditFunction(FuncDeclItem node)
        {
            this._nodalView.EditFunction(node);
        }

        public SearchBar SearchBar = null;

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
                (this.ZoomPanel.RenderTransform as ScaleTransform).ScaleX = e.NewValue;
                (this.ZoomPanel.RenderTransform as ScaleTransform).ScaleY = e.NewValue;

              /* if (((int)(e.NewValue * 10.0) % 2) == 0)
                {*/
                    //this.ZoomPanel.Width = this._nodalView.MainGrid.Width * e.NewValue;
                    //this.ZoomPanel.Height = this._nodalView.MainGrid.Height * e.NewValue;
              //  }
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
        private void WinGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // TODO @Sev: use the fixed version of code ;)
            //if (e.MiddleButton == MouseButtonState.Pressed) // TODO keybard ?
            //{
            //    if (this.ZoomPanel != null && this._nodalView != null)
            //    {
            //        _zoomLevel += e.Delta;
            //        _zoomLevel = Math.Max(_zoomLevel, 0);
            //        this.ZoomPanel.Width = this._nodalView.MainGrid.Width * _zoomLevel / 1000;
            //        this.ZoomPanel.Height = this._nodalView.MainGrid.Height * _zoomLevel / 1000;
            //    }
            //}
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

        private void ZoomPanel_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.IsDown && e.Key == Key.LeftCtrl)
                MessageBox.Show("Bloub a vaincu le monde des licornes");
        
        }
    }
}
