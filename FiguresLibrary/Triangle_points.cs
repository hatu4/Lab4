using System.Drawing;

namespace FiguresLibrary
{
    public class Triangle_points
    {
        public string name;
        public Point[] points = new Point[3];
        public Triangle_points(string name, int x1, int x2, int x3, int y1, int y2, int y3)
        {
            this.name = name;
            this.points[0].X = x1;
            this.points[0].Y = y1;
            this.points[1].X = x2;
            this.points[1].Y = y2;
            this.points[2].X = x3;
            this.points[2].Y = y3;
        }
    }
}
