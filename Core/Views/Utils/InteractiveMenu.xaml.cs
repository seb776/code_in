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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.Utils
{
    /// <summary>
    /// Interaction logic for InteractiveMenu.xaml
    /// </summary>
    public partial class InteractiveMenu : UserControl
    {
        public InteractiveMenu()
        {
            InitializeComponent();
            MouseRightButtonUp += InteractiveMenu_MouseRightButtonUp;
            MouseMove += InteractiveMenu_MouseMove;
        }

        void InteractiveMenu_MouseMove(object sender, MouseEventArgs e)
        {
            this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.LineSelect.Y1 = this.DesiredSize.Height / 2;

            this.LineSelect.Y2 = Math.Min(e.GetPosition(this).Y, this.DesiredSize.Height) - 1;
            this.LineSelect.X2 = e.GetPosition(this).X;
        }

        void InteractiveMenu_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsOpen = false;
            ReleaseMouseCapture();
        }

        public void AddElement(String name)
        {
            SolidColorBrush[] colors = {
                                           new SolidColorBrush(Colors.Magenta),
                                           new SolidColorBrush(Colors.DodgerBlue),
                                           new SolidColorBrush(Colors.Yellow),
                                           new SolidColorBrush(Color.FromArgb(0xFF,0x42,0x42,0x42)),
                                       };
            var lbl = new Label();
            lbl.Content = name;
            lbl.Foreground = new SolidColorBrush(Colors.White);
            lbl.Background = colors[this.ElementsField.Children.Count % 4];
            lbl.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbl.Width = 150;
            lbl.Margin = new Thickness(0, 5, 0, 0);
            this.ElementsField.Children.Add(lbl);
            this._alignProperly();
        }

        private void _alignProperly()
        {
            int multiplier = 5;
            for (int i = 0, j = this.ElementsField.Children.Count; i < this.ElementsField.Children.Count; ++i, --j)
            {
                if (i > (this.ElementsField.Children.Count / 2))
                    (this.ElementsField.Children[i] as Label).Margin = new Thickness(j * multiplier, (this.ElementsField.Children[i] as Label).Margin.Top, 0, 0);
                else
                    (this.ElementsField.Children[i] as Label).Margin = new Thickness(i * multiplier, (this.ElementsField.Children[i] as Label).Margin.Top, 0, 0);
            }
        }

        static Popup _parentPopup = null;

        //Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(InteractiveMenu));
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        //PlacementTarget
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(InteractiveMenu));
        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        //PlacementRectangle
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(InteractiveMenu));
        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }

        //HorizontalOffset
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(InteractiveMenu));
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        //VerticalOffset
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(InteractiveMenu));
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(
                typeof(InteractiveMenu), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InteractiveMenu ctrl = (InteractiveMenu)d;

            if ((bool)e.NewValue)
            {
                if (_parentPopup == null)
                {
                    _parentPopup = new Popup();
                    _parentPopup.AllowsTransparency = true;
                }
                Popup.CreateRootPopup(_parentPopup, ctrl);
                ctrl.CaptureMouse();
            }
        }
    }
}
