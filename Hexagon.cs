using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
/*
 * xml code:
< !--< Path x: Name = "hexagon" Stroke = "White" StrokeThickness = "1" Fill = "Transparent" Height = "500" Width = "500"
              Loaded = "hexagon_Loaded" /> --> 
*/

/* 
 * in codebehind
        private void hexagon_Loaded(object sender, RoutedEventArgs e)
        {
            Hexagon hex = new Hexagon(sender as Path, 1, 200, 200);
            hex.highlight();
        }
 */

namespace DesktopProjectsOrganizerWPF
{
    class Hexagon
    {
        int strokeThickness;
        Path hexPath;
        public Hexagon(Path sender, int StrokeThickness, int Width, int Height)
        {
            strokeThickness = StrokeThickness;
            hexPath = sender;

            CreateDataPath(Width, Height);
        }

        public void highlight()
        {
            BrushConverter bc = new BrushConverter();
            hexPath.Fill = (Brush)bc.ConvertFrom("#a0a0a0");
        }

        PathFigure figure;
        private void CreateDataPath(double width, double height)
        {
            height -= strokeThickness;
            width -= strokeThickness;

            PathGeometry geometry = new PathGeometry();
            figure = new PathFigure();


            figure.StartPoint = new Point(0.25 * width, 0);
            AddPoint(0.75 * width, 0);
            AddPoint(width, 0.5 * height);
            AddPoint(0.75 * width, height);
            AddPoint(0.25 * width, height);
            AddPoint(0, 0.5 * height);

            figure.IsClosed = true;
            geometry.Figures.Add(figure);
            hexPath.Data = geometry;
        }

        private void AddPoint(double x, double y)
        {
            LineSegment segment = new LineSegment();
            segment.Point = new Point(x + 0.5 * hexPath.StrokeThickness,
                y + 0.5 * hexPath.StrokeThickness);
            figure.Segments.Add(segment);
        }

    }
}
