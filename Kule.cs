using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill
{
	 
	public class Kule : GameObject
	{
		const int kKuleIntervall = 33;
		public int KuleIntervall = kKuleIntervall;
		
		public Kule(int x, int y)
		{
			ImageBounds.Width = 10;
			ImageBounds.Height = 15;
			Posisjon.X = x;
			Posisjon.Y = y;
		}

		public void Reset()
		{
			if (Form1.ActiveForm != null)
			{
				Posisjon.Y = Form1.ActiveForm.ClientRectangle.Bottom;
				MovingBounds.Y = Posisjon.Y;
			}

			KuleIntervall = kKuleIntervall;
		}

		public void Sakte()
		{
//		  KuleIntervall = 3;
		}


		public override void Draw(Graphics g)
		{
			UpdateBounds();
			g.FillRectangle(Brushes.IndianRed , MovingBounds);
			Posisjon.Y -= KuleIntervall;
		}

	}
}
