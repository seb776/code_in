using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace code_in.Views 
{
    public class Code_inLink : Shape
    {
        public Code_inLink()
        {
            this.IsHitTestVisible = false;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.Stretch = System.Windows.Media.Stretch.None;
        }

        public enum ELineMode {
            LINE = 0,
            BEZIER = 1
        }

        public ELineMode _lineMode = ELineMode.BEZIER;
        PathGeometry _line = new PathGeometry();
        public double _x1 { get; set; }
        public double _y1 { get; set; }
        public double _x2 { get; set; }
        public double _y2 { get; set; }



        public PathGeometry returnLine()
        {
            _line.Figures.Clear();
            PathFigure pf = new PathFigure();
            PathSegment line = null;
            if (_lineMode == ELineMode.LINE)
                line = new LineSegment(new Point(_x2, _y2), true);
            else if (_lineMode == ELineMode.BEZIER)
            {
                line = new BezierSegment(new Point(_x1 + 100, _y1), new Point(_x2 - 100, _y2), new Point(_x2, _y2), true);
            }

            pf.StartPoint = new Point(_x1, _y1);
            pf.Segments.Add(line);
            _line.Figures.Add(pf);
            return _line;
        }



        protected override Geometry DefiningGeometry
        {
            get
            {
                return returnLine();
            }
        }

        public void changeLineMode(ELineMode lm)
        {
            _lineMode = lm;
            this.InvalidateVisual();
        }

        public void changeLineMode()
        {
            _lineMode = (_lineMode == ELineMode.LINE ? ELineMode.BEZIER : ELineMode.LINE);
        }

    }
}
