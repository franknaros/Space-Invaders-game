using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace UbatSpill
{
	
	public class Score
	{
		public int Count = 0;
		public Point Position = new Point(0,0);
		public Font MyFont = new Font("Compact", 20.0f, GraphicsUnit.Pixel );

		public Score(int x, int y)
		{
			
			Position.X = x;
			Position.Y = y;
		}


		public bool GameOver = false;

		public virtual void Draw(Graphics g)
		{
			if (GameOver == false)
				g.DrawString("SCORE: " + Count.ToString(), MyFont, Brushes.Magenta, Position.X, Position.Y, new StringFormat());
			else
				g.DrawString("Game Over - FINAL SCORE: " + Count.ToString(), MyFont, Brushes.Chocolate, Position.X - 100, Position.Y, new StringFormat());

		}

		public Rectangle GetFrame()
		{
			Rectangle myRect = new Rectangle(Position.X, Position.Y, (int)MyFont.SizeInPoints*Count.ToString().Length, MyFont.Height);
			return myRect;
		}




		
		public void Reset()
		{
			Count = 0;
		}

		/// <summary>
		/// Increments the score by 1
		/// </summary>
		public void Increment()
		{
			Count++;
		}

		public void AddScore(int val)
		{
			Count += val;
		}
	}
}
