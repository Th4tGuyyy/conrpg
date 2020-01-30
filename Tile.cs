using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Tile : GameData
	{
		//holds gylf for tile
		//and entity for object ontop?
		//use this to remove"Entity" list, to keep the xy the same as the map[,]
		public Glyph glyph;
		public bool isSolid = false;
		public Actor entity;

		/// <summary>Render glyph and entity above</summary>
		public void render(int x, int y)
		{
			if(entity != null) {
				int bgcolor = entity.sprite.bgColor;
				if(bgcolor < 0)
					bgcolor = glyph.bgColor;
				GAME.Engine.WriteText(new Point(x, y), entity.sprite.character, entity.sprite.fgColor, bgcolor);
			}
			else {
				Glyph.setGlyph(new Point(x, y), glyph);
			}
		}
	}
}
