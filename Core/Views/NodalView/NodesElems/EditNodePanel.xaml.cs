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
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                FirstBorder.IsEnabled = true;
                FirstBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(FirstBorder, 1);
                Grid.SetColumn(_modifiersArea, 2);
            }
/*            else
            {
                _accessModifiers.IsEnabled = false;
                _accessModifiers.Visibility = System.Windows.Visibility.Hidden;
            }*/
            if ((actions & ENodeActions.MODIFIERS) == ENodeActions.MODIFIERS)
            {
                modifiersArea = true;
                _modifiersArea.Visibility = System.Windows.Visibility.Visible;
                _modifiersArea.IsEnabled = true;
                _modifiersList.IsEnabled = true;
                _modifiersList.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(_modifiersList, 2);
            }
            if ((actions & ENodeActions.ATTRIBUTE) == ENodeActions.ATTRIBUTE) // TODO mais pas forcement pour la beta
            {
            }
            if ((actions & ENodeActions.COMMENT) == ENodeActions.COMMENT) // @Seb on oublie pour la beta
            {
                _commentArea.Visibility = System.Windows.Visibility.Visible;
                _commentArea.IsEnabled = true;
                FourthBorder.Visibility = System.Windows.Visibility.Visible;
                FourthBorder.IsEnabled = true;
                Grid.SetColumn(FourthBorder, 7);
                Grid.SetColumn(_commentArea, 8);
            }
            if ((actions & ENodeActions.GENERICS) == ENodeActions.GENERICS)
            {
                DeclGenericsField.IsEnabled = true;
                DeclGenericsField.Visibility = System.Windows.Visibility.Visible;
                SecondBorder.IsEnabled = true;
                SecondBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(SecondBorder, 3);
                Grid.SetColumn(DeclGenericsField, 4);
            }
            if ((actions & ENodeActions.INHERITANCE) == ENodeActions.INHERITANCE)
            {
                InheritanceField.IsEnabled = true;
                InheritanceField.Visibility = System.Windows.Visibility.Visible;
                ThirdBorder.IsEnabled = true;
                ThirdBorder.Visibility = System.Windows.Visibility.Visible;
                Grid.SetColumn(ThirdBorder, 5);
                Grid.SetColumn(InheritanceField, 6);
            }
            if ((actions & ENodeActions.NAME) == ENodeActions.NAME)
            {
                // already set always visible because all nodes will need a setName
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

        private void DeleteGeneric(object sender, RoutedEventArgs e)
        {

        }

        private void AddGeneric(object sender, RoutedEventArgs e)
        {

        }
    }
}
