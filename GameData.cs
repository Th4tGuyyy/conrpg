using ConsoleGameEngine;
using System.Collections.Generic;
using System;

namespace rpgtest2020
{
	internal class GameData
	{
		public static ConsoleGame GAME;
		public static VirtualConsole VConsole = new VirtualConsole(0, 20, 35, 10);
		public static String METALOCATION = @"D:\Documents\GitHub\conrpg\MetaTags\";
		public static String MAPSLOCATION = @"D:\Documents\GitHub\conrpg\Maps\";

		public static Player player = new Player(null, "Boddy", new Glyph("@", Palettes.DARK_BLUE), new Point(0, 0), 10);
		public static Dictionary<String, Level> allLevels = new Dictionary<string, Level>();
	}
}