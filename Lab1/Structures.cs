using System.Drawing;


namespace Lab1
{
    public partial class Vector
    {
        public Vector() { }
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x = 0;
        public int y = 0;
    };

    public partial class Module
    {
        public Module() { }
        public Module(string Name)
        {
            this.Name = Name;
        }
        public bool Combine(Module a)
        {
            bool isDone = isCreated;
            Name += " " + a.Name;
            isCreated = true;
            size++;
            return isDone == false;
        }
        public void Create()
        {
            isCreated = true;
        }
        public string Name = "";
        public bool isCreated = false;
        public int size = 1;
    }
    public partial class Line
    {
        public Line() { }
        public Line(Vector x, Vector y)
        {
            this.x = x;
            this.y = y;
        }
        public Line(Vector x, Vector y, Color c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }
        public Vector x = new Vector();
        public Vector y = new Vector();
        public Color c = Color.Black;
    }
    public partial class Arc
    {
        public Arc() { }
        public Arc(Rectangle rect, int startAngle, int endAngle)
        {
            this.rect = rect;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
        }
        public Rectangle rect = new Rectangle();
        public int startAngle = 0;
        public int endAngle = 0;
    }
    public partial class TextGraphics
    {
        public TextGraphics() { }
        public TextGraphics(string s, Font f, Vector x)
        {
            this.s = s;
            this.f = f;
            this.x = x;
        }
        public TextGraphics(string s, Font f, Vector x, Color c)
        {
            this.s = s;
            this.f = f;
            this.x = x;
            this.c = c;
        }
        public Vector x = new Vector();
        public string s = "";
        public Font f = new Font("TimesNewRoman", 8);
        public Color c = Color.Black;
    }
}
