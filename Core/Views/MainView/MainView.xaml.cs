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

        public void SetDynamicResources(String keyPrefix)
        {

        }

        public void OpenFile(String filePath)
        {
            this._nodalView.OpenFile(filePath);
        }

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public SearchBar SearchBar = null;

        public MainView(ResourceDictionary resourceDict)
        {
            this._themeResourceDictionary = resourceDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            this.SearchBar = new SearchBar(this.GetThemeResourceDictionary());
            this.SearchBar.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            this.SearchBar.SetValue(WidthProperty, Double.NaN); // Width auto
            this.WinGrid.Children.Add(this.SearchBar);
            this._nodalView = new NodalView.NodalView(this._themeResourceDictionary);
            this.ZoomPanel.Child = this._nodalView;
        }
        public MainView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        private void SliderZoom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.ZoomPanel != null && this._nodalView != null)
            {
                if (((int)(e.NewValue * 10.0) % 2) == 0)
                {
                    this.ZoomPanel.Width = this._nodalView.MainGrid.Width * e.NewValue;
                    this.ZoomPanel.Height = this._nodalView.MainGrid.Height * e.NewValue;
                }
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
                this.ScrollView.ScrollToHorizontalOffset(this.ScrollView.HorizontalOffset + actualDiff.X);
                this.ScrollView.ScrollToVerticalOffset(this.ScrollView.VerticalOffset + actualDiff.Y);
                _lastMousePosFromWinGrid = e.GetPosition(this.WinGrid);
                //_lastMousePosFromWinGrid = actualDiff;
            }
        }
        private void WinGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed) // TODO keybard ?
            {
                if (this.ZoomPanel != null && this._nodalView != null)
                {
                    _zoomLevel += e.Delta;
                    _zoomLevel = Math.Max(_zoomLevel, 0);
                    this.ZoomPanel.Width = this._nodalView.MainGrid.Width * _zoomLevel / 1000;
                    this.ZoomPanel.Height = this._nodalView.MainGrid.Height * _zoomLevel / 1000;
                }
            }
        }
    }
}
