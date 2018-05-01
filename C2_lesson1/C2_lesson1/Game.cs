using System;
using System.Windows.Forms;
using System.Drawing;

namespace C2_lesson1
{

    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }
        internal static Asteroid[] Asteroids { get => _asteroids; set => _asteroids = value; }

        public static Star[] _obj;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        static Image img;
        
        static Game()
        {
            img = Image.FromFile(@"kosmos.jpg");
        }
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.Width;
            Height = form.Height;
            
            g.DrawImage(img, new Rectangle(0, 0, Width, Height));
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();

            timer.Tick += Timer_Tick;

        }
        public static void Load()
        {
            _obj = new Star[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (int i = 0; i < _obj.Length; i++)
            {
                int r = rnd.Next(5, 25);
                _obj[i] = new Star(new Point(150, rnd.Next(r, r)), new Point(-r, r), new Size(2, 2));
            }
            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }
        

        public static void Draw()
        {
            Buffer.Graphics.DrawImage(img, new Rectangle(0, 0, 800, 600));
            foreach (Star obj in _obj)
                obj.Draw();

            foreach (Asteroid a in _asteroids)
            {
                a.Draw();
            }
            _bullet.Draw();
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (Star a in _obj)
               a.Update();
            foreach(Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet)) { System.Media.SystemSounds.Hand.Play(); }
            }
            _bullet.Update();
        }
        public static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        
        
    }
}
