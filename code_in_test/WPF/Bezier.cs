using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace code_in_test.WPF
{
    public class Bezier
    {
        private Panel rootLayout_;
        PathFigure pthFigure;
        PolyBezierSegment qbzSeg;
        PathSegmentCollection myPathSegmentCollection;
        PathFigureCollection pthFigureCollection;
        PathGeometry pthGeometry;
        Point b_;
        Path arcPath;
        public Bezier(Panel rootLayout, Point a, Point b)
        {
            b_ = b;
            rootLayout_ = rootLayout;
            pthFigure = new PathFigure();
            pthFigure.StartPoint = a;

            qbzSeg = new PolyBezierSegment();
            qbzSeg.Points.Add(new Point(a.X + 100, a.Y));
            qbzSeg.Points.Add(new Point(b.X - 100, b.Y));
            qbzSeg.Points.Add(b);
            


            myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(qbzSeg);

            pthFigure.Segments = myPathSegmentCollection;

            pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;
            arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Cyan);
            arcPath.StrokeThickness = 1;
            arcPath.Data = pthGeometry;
            rootLayout.Children.Add(arcPath);

        }


        public void setPositions(Point a, Point b)
        {
            if (b.X >= 0)
            {
                b_ = b;
            }
            if (a.X >= 0)
            {
                pthFigure.StartPoint = a;

            }
            else
            {
                a = pthFigure.StartPoint;
            }
            qbzSeg = new PolyBezierSegment();
            qbzSeg.Points.Add(new Point(a.X + 100, a.Y));
            qbzSeg.Points.Add(new Point(b_.X - 100, b_.Y));
            qbzSeg.Points.Add(b_);
            myPathSegmentCollection.Clear();
            myPathSegmentCollection.Add(qbzSeg);
        }

        public void Delete()
        {
            rootLayout_.Children.Remove(arcPath);
        }
    }
}
