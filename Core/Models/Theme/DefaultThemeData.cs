using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace code_in.Models.Theme
{
    public class DefaultThemeData : IThemeData
    {

        SolidColorBrush ForegroundColor = new SolidColorBrush(Colors.GreenYellow);
        SolidColorBrush BackgroundColor = new SolidColorBrush(Colors.Gray);
        SolidColorBrush NodeTitleColor = new SolidColorBrush(Colors.Green);
        SolidColorBrush NodeItemColor = new SolidColorBrush(Colors.Honeydew);
        

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
       //public int getIntColor();
    }
}
