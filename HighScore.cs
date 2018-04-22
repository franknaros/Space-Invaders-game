using System;
using System.Drawing;
using System.IO;

namespace UbatSpill
{
	
	public class HighScore : Score
	{
        string Navn = welcomescreen.NAVN;

        public HighScore(int x, int y) : base(x, y)
		{
			
		}

		public override void Draw(Graphics g)
		{
            g.DrawString("    NAVN: " +  Navn, MyFont, Brushes.Yellow, Position.X-20, Position.Y-20, new StringFormat());
            g.DrawString(" HIGH SCORE: " + Count.ToString(), MyFont, Brushes.Yellow, Position.X, Position.Y, new StringFormat());
		}

		public void Skriv(int theScore)
		{
			Les();
			if (Count < theScore)
			{
				Count = theScore;
				StreamWriter sw = new StreamWriter("highscore.txt", false);
				sw.WriteLine( Navn +"-"+ Count.ToString());
				sw.Close();
			}
		}


		public void Les()
		{
		  if (File.Exists("highscore.txt"))
		  {
			StreamReader sr = new StreamReader("highscore.txt");
			string score = sr.ReadLine();
                string[] array = score.Split('-');
                Count = Convert.ToInt32(array[1]);
			sr.Close();
		  }
		}

	}
}
