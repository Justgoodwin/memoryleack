using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace C2_lesson1
{

    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer _timer = new Timer();
        public static Random rnd = new Random();


        #region инициализация вской ереси
        public static int Asteroidscount = 1;
        public static Star[] _obj;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static List<Asteroid> _asteroids = new List<Asteroid>();
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(50, 50));
        private static IFAK[] _ifak;
        static Image img;
        #endregion

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

            Ship.MessageDie += Finish;

            form.KeyDown += FormKeyDown;
        }

        public static void CreateAsteroids(int AsteroidsCount)
        {
            for (int i = 0; i < Asteroidscount; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r)));
            }
        }

        public static void Load()
        {
            _obj = new Star[30];
            _ifak = new IFAK[4];
            var rnd = new Random();
            for (int i = 0; i < _obj.Length; i++)
            {
                int r = rnd.Next(5, 25);
                _obj[i] = new Star(new Point(150, rnd.Next(r, r)), new Point(-r, r), new Size(2, 2));
            }

            CreateAsteroids(Asteroidscount);
            
            
            for (int i = 0; i < _ifak.Length; i++)
            {
                int r = rnd.Next(5, 30);
                _ifak[i] = new IFAK(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }
        

        public static void Draw()
        {
            Buffer.Graphics.DrawImage(img, new Rectangle(0, 0, 800, 600));

            foreach (Star obj in _obj)
                obj.Draw();

            foreach (Asteroid ast in _asteroids) ast?.Draw();

            foreach (Bullet b in _bullets) b?.Draw();

            _ship?.Draw();

            if (_ship != null)
                Buffer.Graphics.DrawString("Energy" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);

            foreach(IFAK ifak in _ifak)
            {
                ifak?.Draw();
            }
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (Star s in _obj) s?.Update();

            foreach (Bullet b in _bullets) b?.Update();

            foreach (Asteroid ast in _asteroids) ast?.Update();

            for (int i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _asteroids[i] = null;
                        _bullets.RemoveAt(j);
                        j--;
                    }

                }

                if (_asteroids[i] == null)  //проверка на наличие астероидов
                {
                    CreateAsteroids(Asteroidscount + 1); //добавление астероидов в List
                }

                if (_asteroids[i] == null || !_ship.Collision(_asteroids[i])) continue;

                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
            
        }
        public static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        
        private static void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        
    }
}
