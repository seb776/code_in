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
    /// Logique d'interaction pour ClassNodeGeneric.xaml
    /// </summary>
    public partial class GenericItem : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        List<Tuple<string, EGenericVariance>> GenericsAndTypes;

        public enum EGenericVariance { NOTHING, IN, OUT };

        public GenericItem(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            GenericsAndTypes = new List<Tuple<string, EGenericVariance>>();
            InitializeComponent();
        }
        public GenericItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }


        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(String keyPrefix)
        {

        }

        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        // This part add all generics into a List and call the method to diplay it on node
        public void SetGenerics(String[] generics)
        {
            Tuple<string, EGenericVariance> tmp;

            GenericsNames.Content = "";
            foreach (string mod in generics)
            {
                string GName = mod.Substring((mod.IndexOf(' ') + 1), (mod.Length - (mod.IndexOf(' ') + 1)));
                if (mod.Contains("in"))
                    tmp = new Tuple<string, EGenericVariance>(GName, EGenericVariance.IN);
                else if (mod.Contains("out"))
                    tmp = new Tuple<string, EGenericVariance>(GName, EGenericVariance.OUT);
                else
                    tmp = new Tuple<string, EGenericVariance>(GName, EGenericVariance.NOTHING);
                GenericsAndTypes.Add(tmp);
                SetAffGeneric(tmp);
                if (mod != generics[generics.Length - 1]) // here the separator part
                    GenericsNames.Content += " | ";
            }
        }

        // This part is for the display on the label content
       public void SetAffGeneric(Tuple<string, EGenericVariance> tmp)
        {
            GenericsNames.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xA2, 0xFF));
            if (tmp.Item2 == EGenericVariance.NOTHING)
                GenericsNames.Content += tmp.Item1;
            else
                GenericsNames.Content += tmp.Item2.ToString().ToLower() + " " + tmp.Item1;
        }
    }
}
