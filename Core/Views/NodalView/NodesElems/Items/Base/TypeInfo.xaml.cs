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

namespace code_in.Views.NodalView.Nodes.Items.Base
{
    /// <summary>
    /// Interaction logic for TypeInfo.xaml
    /// </summary>
    public partial class TypeInfo : UserControl, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary = null;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public TypeInfo(ResourceDictionary resDict)
        {
            InitializeComponent();
        }
        public TypeInfo() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }
        public void SetTypeFromString(String type)
        {
            // Not implemented yet
        }
    }
}
