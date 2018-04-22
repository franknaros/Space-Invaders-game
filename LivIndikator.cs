using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UbatSpill
{
    class LivIndikator : GameObject
    {
        public const int MAX_LIVES = 3;

        int _AntallLiv = MAX_LIVES;
        Font LivesFont = new Font("Compact", 20.0f);

        public LivIndikator(int x, int y)
            : base("hovedFartoy.gif")
        {
            this.Posisjon = new Point(x, y);
        }

        public void DecrementLives()
        {
            _AntallLiv--;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawString("LIVES", LivesFont, Brushes.Red, Posisjon);

            if (_AntallLiv > 0)
            {
                g.DrawImage(Bildet, Posisjon.X + 100, Posisjon.Y, Bildet.Width * 2 / 3, Bildet.Height * 2 / 3);
            }

            if (_AntallLiv > 1)
            {
                g.DrawImage(Bildet, Posisjon.X + 104 + Bildet.Width * 2 / 3, Posisjon.Y, Bildet.Width * 2 / 3, Bildet.Height * 2 / 3);
            }

            if (_AntallLiv > 2)
            {
                g.DrawImage(Bildet, Posisjon.X + 108 + 2 * Bildet.Width * 2 / 3, Posisjon.Y, Bildet.Width * 2 / 3, Bildet.Height * 2 / 3);
            }
        }
    }
}
