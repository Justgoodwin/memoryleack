using System.Drawing;

namespace C2_lesson1
{
    class Star : BaseObject
    {
        private Image img;

        public Star(Point pos, Point dir, Size size): base(pos, dir, size)
        {
            img = Image.FromFile(@"index.png");
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X - Size.Width, Pos.X, Pos.Y, Pos.Y + Size.Height);

        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

    }
}
