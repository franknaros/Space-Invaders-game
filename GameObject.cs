using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UbatSpill
{
	
	public class GameObject
	{
        protected Image Bildet = null;
        public Point Posisjon = new Point(60, 60);
        protected Rectangle ImageBounds = new Rectangle(0,0, 10, 10);
        protected Rectangle MovingBounds = new Rectangle();

      public GameObject(string fileName)
       {
        Bildet = Image.FromFile(fileName);
        ImageBounds.Width = Bildet.Width;
        ImageBounds.Height = Bildet.Height;
       }

		public GameObject()
		{
			Bildet = null;
		}

        
        public  int Width
        {
            get 
            {
                 return ImageBounds.Width;
            }
        }

        public  int Height
            {
               get 
                {
                 return ImageBounds.Height;
                }
            }


       public virtual int GetWidth()
        {
             return ImageBounds.Width;
        }
        
       public Image GetImage()
        {
            return Bildet;
        }

        public virtual Rectangle GetBounds()
        {
            return MovingBounds;
        }

        public void UpdateBounds()
        {
            MovingBounds = ImageBounds;
            MovingBounds.Offset(Posisjon);
        }

    
        public virtual void Draw(Graphics g)
        {
            UpdateBounds();
            g.DrawImage(Bildet, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
        }


	}
}
