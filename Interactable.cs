using ConsoleGameEngine;

namespace rpgtest2020
{
	internal class Interactable : GameData
	{
		public Glyph sprite;

		public Level level;
		protected Point location;

		public virtual void update()
		{
		}
	}
}