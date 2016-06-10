using code_in.Presenters.Nodal.Nodes;
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

namespace code_in.Views.NodalView
{
    /// <summary>
    /// Interaction logic for EditNodePanel.xaml
    /// </summary>
    public partial class EditNodePanel : UserControl, ICodeInVisual, ICodeInTextLanguage
    {
        public EditNodePanel(ResourceDictionary themeResDict)
        {
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }

        public EditNodePanel() :
            this(Code_inApplication.MainResourceDictionary)
        { /* Do not use this constructor except for tests */}

        /// <summary>
        /// Called when editing a node, it sets the controls that allows specific node's modifications
        /// </summary>
        public void SetFields(INodePresenter nodePresenter)
        {
            var actions = nodePresenter.GetActions();
            bool modifiersArea = false;

            if ((actions & ENodeActions.ACCESS_MODIFIERS) == ENodeActions.ACCESS_MODIFIERS)
            {
                _accessModifiers.IsEnabled = true;
                _accessModifiers.Visibility = System.Windows.Visibility.Visible;
                modifiersArea = true;
            }
            else
            {
                _accessModifiers.IsEnabled = false;
                _accessModifiers.Visibility = System.Windows.Visibility.Hidden;
            }
            if ((actions & ENodeActions.MODIFIERS) == ENodeActions.MODIFIERS)
            {
                modifiersArea = true;
            }
            if ((actions & ENodeActions.ATTRIBUTE) == ENodeActions.ATTRIBUTE)
            {
            }
            if ((actions & ENodeActions.COMMENT) == ENodeActions.COMMENT)
            {
            }
            if ((actions & ENodeActions.GENERICS) == ENodeActions.GENERICS)
            {
            }
            if ((actions & ENodeActions.INHERITANCE) == ENodeActions.INHERITANCE)
            {
            }
            if ((actions & ENodeActions.NAME) == ENodeActions.NAME)
            {
            }
            this._modifiersArea.IsEnabled = modifiersArea;
            this._modifiersArea.Visibility = (modifiersArea ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden);
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            throw new NotImplementedException();
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
