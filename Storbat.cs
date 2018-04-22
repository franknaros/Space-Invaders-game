using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill 
{
	/// <summary>
	/// Summary description for Storbat.
	/// </summary>
	public class Storbat : GameObject
	{
		private Image AnnetBilde1 = null;
		private Image AnnetBilde2 = null;


		public bool Truffet = false;

		public int CountExplosjon = 0;

		public bool Dod = false;

		private int rseed = (int)DateTime.Now.Ticks;
		private Random RandomNumber = null;


		public bool HoyreRetning = true;

		private const int kIntervall = 10;
		private long Counter = 0;

		public Storbat(string i1, string i2, string i3) : base(i1)
		{
			
			AnnetBilde1 = Image.FromFile(i2);
			AnnetBilde2 = Image.FromFile(i3);

			RandomNumber = new Random(rseed);

			Posisjon.X = 20;
			Posisjon.Y = 10;
			UpdateBounds();
		}

		public void Reset()
		{
			Posisjon.X = 20;
			Dod = false;
			Truffet = false;
			PoengsumRegnet = false;
			CountExplosjon = 0;
			UpdateBounds();
		}

		public override void Draw(Graphics g)
		{
			UpdateBounds();

			if (Truffet)
			{
				DrawExplosjon(g);
				DrawNumber(g);
				return;
			}

			if (Counter % 3 == 0)
				g.DrawImage(Bildet, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
			else if (Counter % 3 == 1)
				g.DrawImage(AnnetBilde1, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
			else 
				g.DrawImage(AnnetBilde2, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
		}


		public void SetCounter(long lCount)
		{
			Counter = lCount;
		}

		public void DrawExplosjon(Graphics g)
		{

			if (Dod)
				return;

			CountExplosjon++;
			if (CountExplosjon < 15)
			{
				for (int i = 0; i < 50; i++)
				{
					int xval = RandomNumber.Next(MovingBounds.Width);
					int yval = RandomNumber.Next(MovingBounds.Height);
					xval += Posisjon.X;
					yval += Posisjon.Y;
					g.DrawLine(Pens.White, xval, yval, xval, yval+1);
				}

			}
			else
			{
				Dod = true;
			}
		}

		public void Flytt()
		{
			if (Truffet)
				return;

			Posisjon.X += kIntervall;

			Counter ++;
		}

		public int PoengsumVerdi = 50;
		public bool PoengsumRegnet = false;
		public int RegnPoengsum()
		{
				Random RandomScore = new Random((int)DateTime.Now.Ticks);
				PoengsumVerdi = RandomScore.Next(1, 4) * 50;
				return PoengsumVerdi;
		}

		public void DrawNumber(Graphics g)
		{
			if (Dod == true)
				return;

			g.DrawString(PoengsumVerdi.ToString(), new Font("Ariel", 14, FontStyle.Bold), Brushes.White, MovingBounds, new StringFormat());
		}


	}
}
