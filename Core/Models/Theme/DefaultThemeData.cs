using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models.Theme
{
    public class DefaultThemeData : AThemeData
    {
        public DefaultThemeData() : base()
        {
            ForegroundColor = new Byte[]{255, 255, 0, 0};
            BackgroundColor = new Byte[] { 255, 42, 42, 42 };
            NodeTitleColor = new Byte[] { 255, 0, 0, 0 };
        }
    }
}
