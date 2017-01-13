using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views
{
    public interface IEnvironmentWindowWrapper
    {
        void FocusCode_inWindow();
        void CloseCode_inWindow();
        void SetTitle(string title);
        void UpdateTitleState();
    }
}
