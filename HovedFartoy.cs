using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill
{
	
	public class Fartoy : GameObject
	{

		private int kIntervall = 5;
		public bool Dod = false;

		public Fartoy(): base("hovedFartoy.gif")
		{
			Posisjon.X = 200;
			Posisjon.Y = 400;

		}

		public Point HentKuleStart()
		{
		  Point Starten = new Point(
			  MovingBounds.Left + MovingBounds.Width/2,
			  MovingBounds.Top - 10);

			return Starten;
		}

		int CountExplosjon = 0;
		Random RandomNumber = new Random((int)DateTime.Now.Ticks);
		public void DrawExplosjon(Graphics g)
		{
			CountExplosjon++;
			if (CountExplosjon < 15)
			{
				for (int i = 0; i < 50; i++)
				{
					int xval = RandomNumber.Next(MovingBounds.Width);
					int yval = RandomNumber.Next(MovingBounds.Height);
					xval += Posisjon.X;
					yval += Posisjon.Y;
					g.DrawLine(Pens.Chartreuse, xval, yval, xval, yval+1);
				}
			}
		}

		public bool Truffet = false;



		public void FlyttVenstre()
		{
			Posisjon.X -= kIntervall;
			if (Posisjon.X < 0)
				Posisjon.X = 0;
		}

		public void FlyttHoyre(int nLimit)
		{
			Posisjon.X += kIntervall;
			if (Posisjon.X > nLimit - Width )
				Posisjon.X = nLimit - Width;
		}

		public override void Draw(Graphics g)
		{
			if (Dod)
				return;

			if (Truffet == false)
			{
				base.Draw(g);
			}
			else
			{
				if (CountExplosjon < 15)
					DrawExplosjon(g);
				else
					Dod = true;
			}
		}

		public void Nullstill()
		{
			Truffet = false;
			Dod = false;
			CountExplosjon = 0;
		}


	}
}
