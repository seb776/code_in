using code_in.Views.NodalView.NodesElems;
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

namespace code_in.Views.Utils
{
    /// <summary>
    /// Interaction logic for HexagonalButton.xaml
    /// </summary>
    public partial class HexagonalButton : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        Action<object[]> _btnAction;
        object[] _args;

        public HexagonalButton(ResourceDictionary themeResDict, Action<object[]> btnAction, params object[] args)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
            _btnAction = btnAction;
            _args = args;
            this.SetThemeResources("Default");
        }

        private void HexaButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_btnAction != null)
                _btnAction.Invoke(_args);
        }

        public void SetImage(ImageSource src)
        {
            var bmp = new Image();
            bmp.Source = src;
            bmp.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.Fant);
            bmp.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            bmp.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            bmp.Width = 50;
            bmp.Height = 50;
            HexaGrid.Children.Add(bmp);
            bmp.MouseRightButtonUp += HexaButton_MouseRightButtonUp;
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(string keyPrefix)
        {
            this.HexaButton.SetResourceReference(Path.FillProperty, keyPrefix + "ContextMenuMainColor");
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
    }
}
