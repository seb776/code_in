using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Presenters
{
    public class LanguagePresenter
    {
        public void ApplyLanguage(ResourceDictionary resDict)
        {
            return; // TODO @Seb quick fix
            foreach (var t in resDict.Keys)
            {
                Code_inApplication.LanguageResourcesDictionary[t as String] = resDict[t as String];
            }
        }
    }

}
