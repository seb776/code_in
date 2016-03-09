using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models.Theme
{
    public class DarkThemeData : AThemeData
    {
        public DarkThemeData() : base()
        {
            BNTypeForeGroundColor = new Byte[]{255, 0, 255, 0};
            BNMainColor = new Byte[] { 255, 0, 0, 0 };
            BNNameForeGroundColor = new Byte[] { 255, 84, 84, 84 };
        }
    }
}
