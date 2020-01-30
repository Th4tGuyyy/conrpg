using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Rect
	{

		public Point topLeft = Point.Zero, bottomRight =Point.Zero;
		public int width { get {return bottomRight.X-topLeft.X; } set {bottomRight = new Point(topLeft.X+value,bottomRight.Y); } }

		public int height { get { return bottomRight.Y - topLeft.Y; } set { bottomRight = new Point(bottomRight.X, topLeft.Y+value); } }
		public Rect(Point topLeft, Point bottomRight)
		{
			this.topLeft = topLeft;
			this.bottomRight = bottomRight;
		}

		public Rect(int x, int y, int w, int h)
		{
			this.topLeft = new Point(x,y);
			this.bottomRight = new Point(x + w, y + h);
		}

		public bool inBounds(Point item)
		{
			return (item.X > topLeft.X && item.X < bottomRight.X && item.Y > topLeft.Y && item.Y < bottomRight.Y) ;
		}


	}
}
