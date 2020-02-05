using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Interactable : GameData
	{
		public Glyph sprite;

		protected Level level;
		protected Point location;

		public virtual void update() { }
	}
}
