using ConsoleGameEngine;

namespace rpgtest2020
{
	internal class Teleporter : Interactable
	{
		/*File formatting
		TELEPORT
		TARGET(or current?)
		X
		Y*/

		private Level targetLevel;
		private Point targetPoint;

		public Teleporter(Level owner, Point location, Level targetLevel, Point targetLocation)
		{
			sprite = new Glyph("T", Palettes.GREEN);
			level = owner;
			this.location = location;

			this.targetLevel = targetLevel;
			this.targetPoint = targetLocation;
		}

		public void teleport(Entity entity)
		{
			entity.say("Teleporting");

			if(entity is Player) {
				level.world[player.getLocation().X, player.getLocation().Y].topObject = null;
				targetLevel.world[targetPoint.X, targetPoint.Y].topObject = entity;
				player.hardSetLocation(targetPoint);
				player.level = targetLevel;
			}
			//spawn on top, only interacts when you TRY to walk on it
		}

		public override void update()
		{
			if(sprite != level.world[location.X, location.Y].glyph)
				sprite = level.world[location.X, location.Y].glyph;
		}
	}
}