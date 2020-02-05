using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Teleporter : Interactable
	{
		/*File formatting
		TELEPORT
		TARGET(or current?)
		X
		Y*/

		private Level targetLevel;
		private Point targetPoint;

		public Teleporter(Level owner,Point location, String levelName, Point targetLocation)
		{
			level = owner;
			this.location = location;

			targetLevel = new Level(levelName);
			this.targetPoint = targetLocation;
		}

		public void teleport(Entity entity)
		{
			VConsole.writeLine("Telporting");

			if(entity is Player)
			{
				targetLevel.world[targetPoint.X, targetPoint.Y].topObject = entity;
				currentLevel = targetLevel;
			}
			//spawn on top, only interacts when you TRY to walk on it
		}

		public override void update()
		{
			if (sprite != level.world[location.X, location.Y].glyph)
				sprite = level.world[location.X, location.Y].glyph;
		}
	}
}
