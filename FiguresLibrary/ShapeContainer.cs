using System.Collections.Generic;

namespace FiguresLibrary
{
    public static class ShapeContainer
    {
        public static List<Figure> figureList;
        static ShapeContainer()
        {
            figureList = new List<Figure>();
        }
        public static void AddFigure(Figure figure)
        {
            figureList.Add(figure);
        }
    }
}