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
    /// <summary>
    /// Logique d'interaction pour AIOAnchor.xaml
    /// </summary>
    public partial class AIOAnchor : UserControl, ICodeInVisual, ICodeInTextLanguage
    {
        public enum EOrientation
        {
            LEFT = 0,
            RIGHT = 1,
        }
        private ResourceDictionary _themeResourceDictionary = null;
        private EOrientation _orientation = EOrientation.LEFT;

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
