﻿using ConsoleGameEngine;

namespace rpgtest2020
{
	internal class Tile : GameData
	{
		//holds gylf for tile
		//and topObject for object ontop?
		//use this to remove"Entity" list, to keep the xy the same as the map[,]
		public Glyph glyph;

		public bool isSolid = false;
		public bool isTransparent = true;
		public Interactable topObject;


		public bool tryUpdate()
		{
			if(topObject == null)
				return false;
			else
				topObject.update();
			return true;

		}

		/// <summary>Render glyph and topObject above</summary>
		public void render(int x, int y)
		{
			if(topObject != null) {
				int bgcolor = topObject.sprite.bgColor;
				if(bgcolor < 0)
					bgcolor = glyph.bgColor;
				GAME.Engine.WriteText(new Point(x, y), topObject.sprite.character, topObject.sprite.fgColor, bgcolor);
			}
			else {
				Glyph.setGlyph(new Point(x, y), glyph);
			}
		}

		public void renderHidden(int x, int y)
		{
			GAME.Engine.SetPixel(new Point(x, y), glyph.fgColor, glyph.fgColor, ConsoleCharacter.Dark);
		}

		private int invertColor(int color)
		{
			int newColor = color;

			if(color == Palettes.BLACK) { newColor = Palettes.DARK_GRAY; }
			if(color == Palettes.BLUE) { newColor = Palettes.DARK_BLUE; }
			if(color == Palettes.CYAN) { newColor = Palettes.DARK_CYAN; }
			if(color == Palettes.DARK_BLUE) { newColor = Palettes.BLUE; }
			if(color == Palettes.DARK_CYAN) { newColor = Palettes.CYAN; }
			if(color == Palettes.DARK_GRAY) { newColor = Palettes.GRAY; }
			if(color == Palettes.DARK_GREEN) { newColor = Palettes.GREEN; }
			if(color == Palettes.DARK_MAGENTA) { newColor = Palettes.MAGENTA; }
			if(color == Palettes.DARK_RED) { newColor = Palettes.RED; }
			if(color == Palettes.DARK_YELLOW) { newColor = Palettes.YELLOW; }
			if(color == Palettes.GRAY) { newColor = Palettes.WHITE; }
			if(color == Palettes.GREEN) { newColor = Palettes.DARK_GREEN; }
			if(color == Palettes.MAGENTA) { newColor = Palettes.DARK_MAGENTA; }
			if(color == Palettes.RED) { newColor = Palettes.DARK_RED; }
			if(color == Palettes.WHITE) { newColor = Palettes.GRAY; }
			if(color == Palettes.YELLOW) { newColor = Palettes.DARK_YELLOW; }

			return newColor;
		}
	}
}