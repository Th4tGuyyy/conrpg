using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class StatsPanel
	{
		public Entity owner;
		Rect bounds;
		public StatsPanel(int x, int y, int w, int h) : this(new Point(x,y),new Point(x+w,y+h))
		{
		}

		public StatsPanel(Point topLeft, Point bottomRight)
		{
			bounds = new Rect(topLeft, bottomRight);
		}

		public void render()
		{
			GameData.GAME.Engine.Frame(bounds.topLeft, bounds.bottomRight, GameData.UICOLOR);

			GameData.GAME.Engine.WriteText(new Point(centerX(owner.name), bounds.topLeft.Y), owner.name, GameData.UICOLOR);

			GameData.GAME.Engine.WriteText(new Point(centerX("Avg Attack")+1, bounds.topLeft.Y+3), "Avg Attack", GameData.UICOLOR);
			GameData.GAME.Engine.WriteText(new Point(centerX("3 - 10")+1, bounds.topLeft.Y + 4), "3 - 10", GameData.UICOLOR);

			GameData.GAME.Engine.WriteText(new Point(centerX("Health") + 1, bounds.topLeft.Y + 6), "Health", GameData.UICOLOR);
			GameData.GAME.Engine.WriteText(new Point(centerX(owner.health + "") + 1, bounds.topLeft.Y + 7), owner.health + "", GameData.UICOLOR);

			GameData.GAME.Engine.WriteText(new Point(centerX("Defence") + 1, bounds.topLeft.Y + 9), "Defence", GameData.UICOLOR);
			GameData.GAME.Engine.WriteText(new Point(centerX("7") + 1, bounds.topLeft.Y + 10), "7", GameData.UICOLOR);



			GameData.GAME.Engine.WriteText(new Point(bounds.topLeft.X + 1, bounds.topLeft.Y + 12), "Inventory(i)", GameData.UICOLOR);

			GameData.GAME.Engine.WriteText(new Point(bounds.topLeft.X + 1, bounds.topLeft.Y + 14), "Stats(c)", GameData.UICOLOR);


		}

		int centerX(String str)
		{
			return bounds.topLeft.X + (bounds.width / 2) - (str.Length / 2);
		}

		public void update()
		{

		}
	}
}
