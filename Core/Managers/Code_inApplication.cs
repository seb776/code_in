using code_in.Models.Theme;
using code_in.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in
{
    // This stores the managers of code_in
    public static class Code_inApplication
    {
        private static String[,] _languageWords = null;
        public static String GetLanguageWord(int x, int y)
        {
            if (_languageWords == null)
            {
                _languageWords = new String[2, 12];

                // French
                _languageWords[0, 0] = "Général";
                _languageWords[0, 1] = "Activer le mode tutoriel";
                _languageWords[0, 2] = "Mises à jour";
                _languageWords[0, 3] = "Démarrage";
                _languageWords[0, 4] = "Quotidien";
                _languageWords[0, 5] = "Mensuel";
                _languageWords[0, 6] = "Jamais";
                _languageWords[0, 7] = "Vérifier les mises à jour";
                _languageWords[0, 8] = "Dossier du fichier de configuration";
                _languageWords[0, 9] = "Parcourir";
                _languageWords[0, 10] = "Valider";
                _languageWords[0, 11] = "Annuler";
                // English
                _languageWords[1, 0] = "General";
                _languageWords[1, 1] = "Activate tutorial mode";
                _languageWords[1, 2] = "Updates";
                _languageWords[1, 3] = "Boot";
                _languageWords[1, 4] = "Daily";
                _languageWords[1, 5] = "Monthly";
                _languageWords[1, 6] = "Never";
                _languageWords[1, 7] = "Check updates";
                _languageWords[1, 8] = "Configuration folder";
                _languageWords[1, 9] = "Browse";
                _languageWords[1, 10] = "Ok";
                _languageWords[1, 11] = "Cancel";
            }
            return _languageWords[x, y];
        }
        public static void StartApplication(IEnvironmentWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(wrapper != null, "You must give a valid wrapper in order to start the application !");
            _environmentWrapper = wrapper;
            // Code_inApplication.ThemeMgr.setMainTheme(new DefaultThemeData()); // TODO when theme management is complete and functional, load default theme
        }
        public static IEnvironmentWrapper _environmentWrapper = null;
        private static ThemeMgr _themeMgr = null;

        public static ThemeMgr ThemeMgr
        {
            get
            {
                if (_themeMgr == null)
                    _themeMgr = new ThemeMgr();
                return _themeMgr;
            }
        }
        public static IEnvironmentWrapper EnvironmentWrapper
        {
            get
            {
                return _environmentWrapper;
            }
        }
        public static ResourceDictionary LanguageResourcesDictionary
        {
            get
            {
                if (_languageResourceDictionary == null)
                {
                    Application.ResourceAssembly = Assembly.GetExecutingAssembly(); // TODO @z0rg Maybe we can remove it
                    System.Uri resourceLocater = new System.Uri("/code_inCore;component/Models/LanguageResourcesDictionary.xaml", UriKind.Relative);
                    _languageResourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                }
                return _languageResourceDictionary;
            }
        }
        private static ResourceDictionary _languageResourceDictionary = null;
    }
}
