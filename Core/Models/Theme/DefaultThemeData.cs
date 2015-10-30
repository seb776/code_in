using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace code_in.Models.Theme
{
    public class DefaultThemeData: IThemeData
    {

        SolidColorBrush ForegroundColor = new SolidColorBrush(Colors.GreenYellow);
        SolidColorBrush BackgroundColor = new SolidColorBrush(Colors.Gray);
        SolidColorBrush NodeTitleColor = new SolidColorBrush(Colors.Green);
        SolidColorBrush NodeItemColor = new SolidColorBrush(Colors.Honeydew);
        SolidColorBrush IntColor = new SolidColorBrush(Colors.Pink);
        SolidColorBrush FloatColor = new SolidColorBrush(Colors.Plum);
        SolidColorBrush ShortColor = new SolidColorBrush(Colors.PowderBlue);
        SolidColorBrush CharColor = new SolidColorBrush(Colors.Purple);
        SolidColorBrush BoolColor = new SolidColorBrush(Colors.RosyBrown);
        SolidColorBrush EnumColor = new SolidColorBrush(Colors.Salmon);
        SolidColorBrush ClassColor = new SolidColorBrush(Colors.SeaShell);
        SolidColorBrush FuncDeclColor = new SolidColorBrush(Colors.Magenta);
        SolidColorBrush DeclColor = new SolidColorBrush(Colors.Maroon);

        public SolidColorBrush getNodeForegroundColor()
        {
            return ForegroundColor;
        }

        public SolidColorBrush getNodeBackgroundColor()
        {
            return BackgroundColor;
        }
        public SolidColorBrush getNodeTitleColor()
        {
            return NodeTitleColor;
        }
        public SolidColorBrush getNodeItemColor()
        {
            return NodeItemColor;
        }
        public SolidColorBrush getIntColor()
        {
            return IntColor;
        }
        public SolidColorBrush getFloatColor()
        {
            return FloatColor;
        }
        public SolidColorBrush getShortColor()
        {
            return ShortColor;
        }
        public SolidColorBrush getCharColor()
        {
            return CharColor;
        }
        public SolidColorBrush getBoolColor()
        {
            return BoolColor;
        }
        public SolidColorBrush getEnumColor()
        {
            return EnumColor;
        }
        public SolidColorBrush getClassColor()
        {
            return ClassColor;
        }
        public SolidColorBrush getFuncDeclColor()
        {
        return FuncDeclColor;
        }
        public SolidColorBrush getDeclColor()
        {
            return DeclColor;
        }
        public ILinkDraw getLinkDrawer()
        {
            return null;
        }
    }
}
