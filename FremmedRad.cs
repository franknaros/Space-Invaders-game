using System;
using System.Drawing;

namespace UbatSpill
{
	
	public class FremedFartoy
	{
		public Fremmed[] Fremmede2 = new Fremmed[11];
		public Point SistePosisjon3 = new Point(0, 0);
		public const int kBombIntervalSpacing3 = 50;


		public FremedFartoy(string gif1, string gif2, int rowNum)
		{
			
			for (int i = 0; i < Fremmede2.Length; i++)
			{
			  Fremmede2[i] = new Fremmed(gif1, gif2);
			  Fremmede2[i].Posisjon.X = i * Fremmede2[i].GetBounds().Width + 5;
			  Fremmede2[i].Posisjon.Y = rowNum * Fremmede2[i].GetBounds().Height + 10;
			  Fremmede2[i].SettCounter(i*kBombIntervalSpacing3);
			}

			SistePosisjon3 = Fremmede2[Fremmede2.Length - 1].Posisjon;
		}

		public void NullstillBombeCounter()
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				Fremmede2[i].NullstillBombePosisjon();
				Fremmede2[i].SettCounter(i*kBombIntervalSpacing3);
			}
		}

		public Fremmed this [int index]   
		{
			get 
			{
			  return Fremmede2[index];
			}
		}


		public void Draw(Graphics g)
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				Fremmede2[i].Draw(g);
			}
		}

		public int KollisjonTest(Rectangle aRect)
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				if ((Fremmede2[i].GetBounds().IntersectsWith(aRect)) && (!Fremmede2[i].Truffet))
					return i;
			}

			return -1;
		}

		public bool HoyreRetning
		{
			set
			{
				for (int i = 0; i < Fremmede2.Length; i++)
				{
					Fremmede2[i].HoyreRetning = value;
				}
			}
		}

		public void Flytt()
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				Fremmede2[i].Move();
			}

			if (Fremmede2[0].HoyreRetning)
				SistePosisjon3 = Fremmede2[Fremmede2.Length - 1].Posisjon;
			else
				SistePosisjon3 = Fremmede2[0].Posisjon;

		}

		public void FlyttIPlass()
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				Fremmede2[i].FlyttIPlass();
			}

		}


		public Fremmed HentForsteFremmed()
		{
			int count = 0;
			Fremmed Fremmeden  = Fremmede2[count];
			while ((Fremmeden.Truffet == true) && (count < Fremmede2.Length-1))
			{
			  count++;
			  Fremmeden = Fremmede2[count];
			}

			return Fremmeden;
		}

		public Fremmed HentSisteFremmed()
		{
			int count = Fremmede2.Length - 1;
			Fremmed Fremmeden  = Fremmede2[count];
			while ((Fremmeden.Truffet == true) && (count > 0))
			{
				count--;
				Fremmeden = Fremmede2[count];
			}

			return Fremmeden;
		}

		public int AntallLivFremmede()
		{
			int count = 0;
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				 if (Fremmede2[i].Dod == false)
					 count++;
			}

			return count;
		}

		public bool FremmedHarLandet(int bottom)
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
			  if ( (Fremmede2[i].GetBounds().Bottom >= bottom) &&
				   (Fremmede2[i].Truffet = false))
				  return true;
			}

			return false;
		
		}

		public void FlyttNed()
		{
			for (int i = 0; i < Fremmede2.Length; i++)
			{
				Fremmede2[i].Posisjon.Y += Fremmede2[i].GetBounds().Height/8;
				Fremmede2[i].UpdateBounds();
			}
		}


	}
}
