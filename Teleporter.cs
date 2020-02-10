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

		public override void onInteract(Entity activator)
		{
			teleport(activator);
		}

		public void teleport(Entity entity)
		{
			//entity.say("Teleporting");

			if(entity is Player) {
				level.world[entity.getLocation().X, entity.getLocation().Y].topObject = null;//deletes player
				entity.level = targetLevel;
				entity.move(targetPoint);//moves player to targetlocation
			}
		}

		public override void update()//clears sprite because sprite is manditory, change later?
		{
			if(sprite != level.world[location.X, location.Y].glyph)
				sprite = level.world[location.X, location.Y].glyph;
		}
	}
}