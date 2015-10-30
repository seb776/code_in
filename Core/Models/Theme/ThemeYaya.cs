using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace code_in.Models.Theme
{
    public class ThemeYaya : IThemeData
    {

        SolidColorBrush ForegroundColor = new SolidColorBrush(Colors.Crimson);
        SolidColorBrush BackgroundColor = new SolidColorBrush(Colors.Orchid);
        SolidColorBrush NodeTitleColor = new SolidColorBrush(Colors.Black);
        SolidColorBrush NodeItemColor = new SolidColorBrush(Colors.LimeGreen);


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
            return null;
        }
        public SolidColorBrush getFloatColor()
        {
            return null;
        }
        public SolidColorBrush getShortColor()
        {
            return null;
        }
        public SolidColorBrush getCharColor()
        {
            return null;
        }
        public SolidColorBrush getBoolColor()
        {
            return null;
        }
        public SolidColorBrush getEnumColor()
        {
            return null;
        }
        public SolidColorBrush getClassColor()
        {
            return null;
        }
        public SolidColorBrush getFuncDeclColor()
        {
            return null;
        }
        public SolidColorBrush getDeclColor()
        {
            return null;
        }
        public ILinkDraw getLinkDrawer()
        {
            return null;
        }
    }
}
