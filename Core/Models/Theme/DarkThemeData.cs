using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models.Theme
{
    public class DarkThemeData : ThemeData
    {
        public DarkThemeData()
        {
            ForegroundColor = new Byte[]{255, 0, 255, 0};
            BackgroundColor = new Byte[] { 255, 0, 0, 0 };
            NodeTitleColor = new Byte[] { 255, 84, 84, 84 };
        }
    }
}
