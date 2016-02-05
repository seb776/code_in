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

namespace code_in.Views.MainView
{

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class MainView : UserControl, stdole.IDispatch, ICodeInVisual
    {
        private ViewModels.code_inMgr _code_inMgr;

        private ResourceDictionary _resourceDictionary = null;

        public void OpenFile(String filePath)
        {
            _code_inMgr.LoadFile(filePath);
        }

        NodalView.NodalView _nodalView;
        public NodalView.NodalView NodalView
        {
            get { return _nodalView; }
            private set { _nodalView = value; }
        }
        public ResourceDictionary GetResourceDictionary()
        {
            return this._resourceDictionary;
        }
        public MainView(ResourceDictionary resourceDict)
        {
            this._resourceDictionary = resourceDict;
            this.Resources.MergedDictionaries.Add(this.GetResourceDictionary());
            InitializeComponent();
            _nodalView = new NodalView.NodalView(this.GetResourceDictionary());
            this.ZoomPanel.Child = _nodalView;
            _code_inMgr = new ViewModels.code_inMgr(this);

            //this.MouseWheel += MainView_MouseWheel;
            //this.KeyDown += MainView_KeyDown;
            //this.MouseUp += MainView_MouseUp;
        }

        public MainView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        private void SliderZoom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.NodalView != null)
            {
                if (((int)(e.NewValue * 10.0) % 2) == 0)
                {
                    this.ZoomPanel.Width = this.NodalView.Width * e.NewValue;
                    this.ZoomPanel.Height = this.NodalView.Height * e.NewValue;
                    System.Diagnostics.Trace.WriteLine(this.ZoomPanel.Width.ToString());

                }
            }
        }
    }
}
