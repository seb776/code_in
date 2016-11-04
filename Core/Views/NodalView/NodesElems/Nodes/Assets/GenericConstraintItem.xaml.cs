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

namespace code_in.Views.NodalView.NodesElems.Nodes.Assets
{
    /// <summary>
    /// Logique d'interaction pour GenericConstraintItem.xaml
    /// </summary>
    public partial class GenericConstraintItem : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public GenericConstraintItem(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
        }
        public GenericConstraintItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public void setConstraint(String constraintType, AstNodeCollection<AstType> types)
        {
            this.Generic_symbol.Content = constraintType;
            foreach (var type in types)
            {
                var lbl = new Label();
                lbl.Foreground = new SolidColorBrush(Colors.White);
                lbl.Content = type.ToString();
                this.ConstraintList.Children.Add(lbl);
            }
        }
        public ResourceDictionary GetThemeResourceDictionary()
        {
           return(_themeResourceDictionary);
        }
        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
    }
}
