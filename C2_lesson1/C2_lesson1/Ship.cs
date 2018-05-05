using System.Drawing;

namespace C2_lesson1
{
    
    class Ship:BaseObject
    {
        private Image img;
        private int _energy = 100;
        public int Energy => _energy;

        public static event Messege MessageDie;

        
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public void EnergyRefil(int n)
        {
            if (_energy < 100)
            {
                int f = 100 - n;
                if (f > 75)
                {
                    int z = 100 - f;
                    _energy += z;
                }
                else
                    _energy += n;
            }
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = Image.FromFile(@"ship.png");
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            base.Update();
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
