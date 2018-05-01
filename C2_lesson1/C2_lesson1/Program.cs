using System;
using System.Windows.Forms;

namespace C2_lesson1
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            form.Width = 800;
            form.Height = 600;
            try
            {
                if (form.Height < 1000 && form.Width < 1000 && form.Width > 0 && form.Height > 0)
                {
                    Game.Init(form);
                    form.Show();
                    Game.Load();
                    Game.Draw();
                    Application.Run(form);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Одно из значение больше 1000 или отрицательное");
            }
            

        }
    }
}
