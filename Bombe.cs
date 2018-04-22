using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill
{
	
	public class Bombe : GameObject
	{
		public const int kBombeIntervall3 = 5;
		public int TheBombInterval3 = kBombeIntervall3;

		public Bombe(int x, int y)
		{
			ImageBounds.Width = 5;
			ImageBounds.Height = 15;
			Posisjon.X = x;
			Posisjon.Y = y;
		}


		public override void Draw(Graphics g)
		{
			UpdateBounds();
			g.FillRectangle(Brushes.Yellow, MovingBounds);
			Posisjon.Y += TheBombInterval3;
		}

		public void NullstillBombe(int yPos)
		{
		  Posisjon.Y = yPos;
		  TheBombInterval3 = kBombeIntervall3;
		  UpdateBounds();
		}


	}
}
