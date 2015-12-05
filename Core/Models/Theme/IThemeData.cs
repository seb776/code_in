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
       SolidColorBrush getDeclColor();
       SolidColorBrush getIntColor();
       SolidColorBrush getFloatColor();
       SolidColorBrush getShortColor();
       SolidColorBrush getCharColor();
       SolidColorBrush getBoolColor();
       SolidColorBrush getEnumColor();
       SolidColorBrush getClassColor();
       SolidColorBrush getFuncDeclColor();
       ILinkDraw getLinkDrawer();

       void serializeTheme(string filename);
      // void deserializeTheme(string filename);
       
        /*
        getGridBackground/foreground/thickness/dottedDistance/xSize/ySize
         */

    }
}
