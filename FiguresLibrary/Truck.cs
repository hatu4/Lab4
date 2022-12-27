using System.Collections.Generic;

namespace FiguresLibrary
{
    public class Truck : Rectangle
    {
        public List<Figure> figures;
        public Truck(int id, int x, int y, int w=180, int h=140) : base(id, x, y, w, h)
        {
            name = "Грузовик";
            figures = new List<Figure>();
            Rectangle r1 = new Rectangle(0, x, y + 60, 40, 40);
            Rectangle r2 = new Rectangle(0, x + 40, y, 40, 100);
            Rectangle r3 = new Rectangle(0, x + 80, y + 60, 100, 40);
            Rectangle r4 = new Rectangle(0, x + 46, y + 10, 28, 50);
            Circle c1 = new Circle(0, x  + 10, y + 100, 20);
            Circle c2 = new Circle(0, x + 110, y + 100, 20);
            Figure[] fs = { r1, r2, r3, r4, c1, c2, };
            figures.AddRange(fs);
        }
        public override void Draw()
        {
            Init.pen.Width = 1;
            for (int i = 0; i < figures.Count; i++)
            {
                figures[i].Draw();
            }
            Init.pen.Width = 3;
        }
        public override void MoveTo(int x, int y)
        {
            Init.Clear();
            this.x += x;
            this.y += y;
            for (int i = 0; i < figures.Count; i++)
            {
                figures[i].x += x;
                figures[i].y += y;
            }
            this.Draw();
            foreach (Figure f in ShapeContainer.figureList)
            {
                f.Draw();
            }
        }
    }
}
