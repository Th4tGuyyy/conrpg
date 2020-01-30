using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Glyph : GameData
	{
		public int fgColor = 1;
		public int bgColor = -1;
		public String character = "";

		public Glyph(String str, int fgc, int bgc)
		{
			fgColor = fgc;
			bgColor = bgc;
			character = str;
		}

		public Glyph(String str, int fgc)
		{
			fgColor = fgc;
			bgColor = -1;
			character = str;
		}

		public String toString()
		{
			return $"{character} {fgColor} {bgColor}";
		}

		public static void setGlyph(Point loc, Glyph thing)
		{
			if(thing.bgColor != -1)
				GAME.Engine.WriteText(loc, thing.character, thing.fgColor, thing.bgColor);
			else
				GAME.Engine.WriteText(loc, thing.character, thing.fgColor, GAME.Engine.GetBackground());

		}

	}
}
