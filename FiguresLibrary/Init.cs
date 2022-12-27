using System.Drawing;
using System.Windows.Forms;

namespace FiguresLibrary
{
    public static class Init
    {
        public static Bitmap bitmap;
        public static PictureBox pictureBox;
        public static ListBox figuresListBox;
        public static Pen pen;
        public static BindingSource bs;
        public static void Clear()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.Clear(Color.White);
        }
    }
}
