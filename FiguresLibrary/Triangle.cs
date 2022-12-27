using System.Drawing;

namespace FiguresLibrary
{
    public class Triangle : Polygon
    {
        public Triangle(int id, Point[] points, string u_name=null) : base(id, points)
        {
            unique_name = u_name;
            name = "Треугольник";
        }
    }
}