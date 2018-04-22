using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill 
{
	
	public class Fremmed : GameObject
	{
		private Image OtherImage = null;

		private const int kBombeIntervall = 200;

		private Bombe Bomben = new Bombe(0, 0);

		private bool AktivBombe = false;

		public bool Truffet = false;

		public int CountExplosjon = 0;

		public bool Dod = false;

		private int rseed = (int)DateTime.Now.Ticks;
		private Random RandomNumber = null;


		public bool HoyreRetning = true;

		private const int kIntervall = 10;
		private long Counter = 0;

		public Fremmed(string i1, string i2) : base(i1)
		{
			
			OtherImage = Image.FromFile(i2);

			RandomNumber = new Random(rseed);

			Posisjon.X = 20;
			Posisjon.Y = 10;
			UpdateBounds();
		}

		public override void Draw(Graphics g)
		{
			UpdateBounds();

			if (Truffet)
			{
				DrawExplosion(g);
				return;
			}

			if (Counter % 2 == 0)
				g.DrawImage(Bildet, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
			else
				g.DrawImage(OtherImage, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);

			if (AktivBombe)
			{
			  Bomben.Draw(g);
				if (Form1.ActiveForm != null)
				{
					if (Bomben.Posisjon.Y > Form1.ActiveForm.ClientRectangle.Height)
					{
						AktivBombe = false;
					}
				}
			}


			if ((AktivBombe == false) && (Counter % kBombeIntervall == 0))
			{
				AktivBombe = true;
				Bomben.Posisjon.X = MovingBounds.X + MovingBounds.Width/2;
				Bomben.Posisjon.Y = MovingBounds.Y + 5;
			}

		}

		public void SlowBomb()
		{
//		  Bomben.TheBombInterval = 2;
		}


		public void NullstillBombePosisjon()
		{
			Bomben.Posisjon.X = MovingBounds.X + MovingBounds.Width/2;
			Bomben.NullstillBombe(MovingBounds.Y + 5);
		}

		public void SettCounter(long lCount)
		{
			Counter = lCount;
		}

		public void DrawExplosion(Graphics g)
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

		public void Move()
		{
			if (Truffet)
				return;

			if (HoyreRetning)
			{
				Posisjon.X += kIntervall;
			}
			else
			{
			    Posisjon.X -=kIntervall;
			}

			Counter ++;
		}

		public void FlyttIPlass()
		{
			Counter ++;
		}

		public Rectangle GetBombBounds()
		{
		  return Bomben.GetBounds();
		}

		public bool BombeKolliderer(Rectangle r)
		{
			if (AktivBombe && Bomben.GetBounds().IntersectsWith(r))
			{
			  return true;
			}

			return false;
		}

	}
}
