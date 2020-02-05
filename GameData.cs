using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class GameData
	{
		public static ConsoleGame GAME;
		public static VirtualConsole VConsole = new VirtualConsole(0, 20, 35, 10);
		public static Level currentLevel;
		public static String METALOCATION = @"D:\Documents\GitHub\conrpg\MetaTags\";
		public static String MAPSLOCATION = @"D:\Documents\GitHub\conrpg\Maps\";



	}
}
