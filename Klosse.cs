using System;
using System.Drawing;
using System.Collections;

namespace UbatSpill
{
	/// <summary>
	/// Summary description for Klosse.
	/// </summary>
	public class Klosse : GameObject
	{
		private ArrayList KuleHuller = new ArrayList(50);
		public Klosse(): base("klosse.gif")
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			foreach(Rectangle r in KuleHuller)
			{
				g.FillRectangle(Brushes.Black, r);
			}
		}

		public void LeggTilKuleHull(Rectangle r, bool dir)
		{
		  Rectangle rup = r;
		  rup.Inflate(r.Width/4, -r.Height/4);

			if (dir == true)
			{
				rup.Offset(0, -r.Height/2);
			}
			else
			{
	//			rup.Offset(0, r.Height/2);
				rup.Height += 20;
                
				if ((rup.Y - Posisjon.Y) <= 20)
				{
				  rup.Y = Posisjon.Y;
				  rup.Height += 5;
				}
			}

		  KuleHuller.Add(rup);
		}

		private bool SjekkKuleHullInterseksjon(Rectangle rTest, bool dirDown)
		{
			Rectangle rTest1 = rTest;

			// Kule test
			rTest1.X += rTest1.Width/2;
			rTest1.Width = 3;

			foreach(Rectangle r in KuleHuller)
			{
				if (rTest1.IntersectsWith(r))
				{
					return true;
				}
			}

			return false;
		}

		public bool TestKollisjon(Rectangle r, bool NedRetning, out bool KuleHull)
		{
			KuleHull = false;

			if (SjekkKuleHullInterseksjon(r, NedRetning))
			{
				KuleHull = true;
				return false;  // Teller ikke som en kollisjon
			}

			if (r.IntersectsWith(MovingBounds))
			{
			  LeggTilKuleHull(r, NedRetning);
			  return true;
			}

			return false;
		}


	}
}
