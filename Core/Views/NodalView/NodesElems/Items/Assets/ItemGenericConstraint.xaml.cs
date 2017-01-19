using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using ICSharpCode.NRefactory.CSharp;
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

namespace code_in.Views.NodalView.NodesElems.Items.Assets
{
    /// <summary>
    /// Logique d'interaction pour ItemGenericConstraint.xaml
    /// </summary>
    public partial class ItemGenericConstraint : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public ItemGenericConstraint(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }
        public void setConstraint(String constraintType, AstNodeCollection<AstType> types)
        {
            var constraint = new GenericConstraintItem(this._themeResourceDictionary);
            this.ConstraintsContainer.Children.Add(constraint);
            constraint.setConstraint(constraintType, types);
        }

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
    }
}
