using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.Utils
{
    /// <summary>
    /// Interaction logic for HexagonalMenu.xaml
    /// </summary>
    public partial class HexagonalMenu : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public HexagonalMenu(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
            this.Opacity = 0;
            this.MouseRightButtonUp += HexagonalMenu_MouseRightButtonUp;
            LostMouseCapture += HexagonalMenu_LostMouseCapture;
        }

        void HexagonalMenu_LostMouseCapture(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("BIPBIP");
            //MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Right);
            //args.RoutedEvent = PreviewMouseUpEvent;
            //args.Source = this.GridHexa;
            //RaiseEvent(args);
            ////this.Raise
            IsOpen = false;
        }
        void HexagonalMenu_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            //this.GridHexa.RaiseEvent(e);
        }
        private int _count = 0;
        public void AddHexagonButton(int x, int y, String keyPrefix, ImageSource src, HexagonalButton.ButtonAction action, params object[] args)
        {
            var hexBtn = new HexagonalButton(this.GetThemeResourceDictionary(), action, args);
            hexBtn.SetImage(src);
            this.GridHexa.Children.Add(hexBtn);
            hexBtn.Margin = new Thickness(x * 65 + ((y % 2) == 0 ? 0 : 33), y * 60, 0, 0);
            hexBtn.SetThemeResources(keyPrefix);
            ++_count;
        }

        public void AddHexagonButtonCircle(String keyPrefix, ImageSource src, HexagonalButton.ButtonAction action, params object[] args)
        {
            int[,] buttons = {
                     {1,0},
                     {2,0},
                     {2,1},
                     {2,2},
                     {1,2},
                     {0,1}
                 };
            if (_count < 6)
                AddHexagonButton(buttons[_count, 0], buttons[_count, 1], keyPrefix, src, action, args);
        }

        //public void ShowMenu()
        //{
        //    DoubleAnimation da = new DoubleAnimation();

        //    da.From = 0;
        //    da.To = 1;
        //    da.Duration = new Duration(TimeSpan.FromMilliseconds(150));
        //    this.BeginAnimation(OpacityProperty, da);
        //}

        static Popup _parentPopup = null;

        //Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(HexagonalMenu));
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        //PlacementTarget
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(HexagonalMenu));
        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        //PlacementRectangle
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(HexagonalMenu));
        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }

        //HorizontalOffset
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(HexagonalMenu));
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        //VerticalOffset
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(HexagonalMenu));
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(
                typeof(HexagonalMenu), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexagonalMenu ctrl = (HexagonalMenu)d;

            if ((bool)e.NewValue)
            {
                if (_parentPopup == null)
                {
                    _parentPopup = new Popup();
                    _parentPopup.AllowsTransparency = true;
                }
                Popup.CreateRootPopup(_parentPopup, ctrl);
                DoubleAnimation da = new DoubleAnimation();

                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(150));
                ctrl.BeginAnimation(OpacityProperty, da);
                Mouse.Capture(ctrl, CaptureMode.SubTree);
            }
        }
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

    }
}
