using ConsoleGameEngine;
using System;

namespace rpgtest2020
{
	internal class Glyph : GameData
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

		public String toString() => $"{character} {fgColor} {bgColor}";

		public static void setGlyph(Point loc, Glyph thing)//renders glyph, used for debug
		{
			if(thing.bgColor != -1)
				GAME.Engine.WriteText(loc, thing.character, thing.fgColor, thing.bgColor);
			else
				GAME.Engine.WriteText(loc, thing.character, thing.fgColor, GAME.Engine.GetBackground());
		}
	}
}