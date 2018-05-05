using System;
using System.Drawing;

//Individual First Aid Kit

namespace C2_lesson1
{
    class IFAK:BaseObject,ICloneable
    {
        private Image img;
        public  int Power { get; set; }

        public IFAK(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = Image.FromFile(@"IFAK.png");
        }
        public object Clone()
        {
            IFAK ifak = new IFAK(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            ifak.Power = Power;
            return ifak;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
