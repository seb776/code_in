using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace code_in.Models.Theme
{
    public interface IThemeData
    {
       SolidColorBrush getNodeForegroundColor();
       SolidColorBrush getNodeBackgroundColor();
       SolidColorBrush getNodeTitleColor();
       SolidColorBrush getNodeItemColor();
   //    int getIntColor();
        /*
getdouble/float/short/char/bool/enum/class..color
getDeclColor
getFuncDecl
getGridBackground/foreground/thickness/dottedDistance/xSize/ySize
getLinkDrawer return ILinkDraw*/

    }
}
