using ConsoleGameEngine;
using System.Collections.Generic;
using System;

namespace rpgtest2020
{
	internal class GameData
	{
		//game consts
		public const int PIXEL_SIZE = 16;
		public const int WINDOW_WIDTH = 40;
		public const int WINDOW_HEIGHT = 37;


		//level position 
		public static int levelXOffset = 1;
		public static int levelYOffset = 1;
		public static int levelViewSize = 21;

		public static ConsoleGame GAME;
		public static VirtualConsole VConsole = new VirtualConsole(0, Level.bounds.bottomRight.Y-1, WINDOW_WIDTH-1, WINDOW_HEIGHT- Level.bounds.bottomRight.Y-1);
		
		public static int UICOLOR = Palettes.DARK_GRAY;

		public static String METALOCATION = @"C:\Users\carso\Source\Repos\conrpg\MetaTags\";
		public static String MAPSLOCATION = @"C:\Users\carso\Source\Repos\conrpg\Maps\";

		public static Player player = new Player(null, "Bobby", new Glyph("@", Palettes.DARK_BLUE), new Point(0, 0), 10);

		public static StatsPanel statsPanel = new StatsPanel(Level.bounds.bottomRight.X - 1, 0, WINDOW_WIDTH - Level.bounds.bottomRight.X, Level.bounds.height - 1) { owner = player };

		public static Dictionary<String, Level> allLevels = new Dictionary<String, Level>();

		public static GameState currentGameState = GameState.RUNNING;
		public enum GameState
		{
			RUNNING,
			MENU,
			STOP,
		};
	}
}