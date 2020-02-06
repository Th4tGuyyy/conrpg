using ConsoleGameEngine;
using ExtendedAscii;
using System;
using System.Collections.Generic;
using System.IO;

namespace rpgtest2020
{
	internal class Level : GameData
	{
		private static int xOffset = 0;
		private static int yOffset = 0;
		private static int viewSize = 20;
		private static Point screenSize = new Point(20, 20);


		private int HEIGHT, WIDTH;

		public Tile[,] world;

		private String path;

		public Level(String path)
		{
			this.path = GameData.MAPSLOCATION + path;
		}
		
		public void loadLevel()//time reminant might be caused if mutliple player spawns?
		{
			try {
				StreamReader sr = new StreamReader(path);

				String[] dimensions = sr.ReadLine().Split(new char[] { ' ' });
				WIDTH = Convert.ToInt32(dimensions[0]);
				HEIGHT = Convert.ToInt32(dimensions[1]);

				//world = new Glyph[WIDTH, HEIGHT];
				world = new Tile[WIDTH, HEIGHT];

				for(int y = 0; y < HEIGHT; y++) {
					String[] line = sr.ReadLine().Split(" ");
					for(int x = 0; x < WIDTH; x++) {
						world[x, y] = new Tile();
						world[x, y].glyph = deHash(line[x], new Point(x, y));
					}
				}
				sr.Close();
			}
			catch {
			}
		}

		private Glyph deHash(String hash, Point loc)
		{
			//0=char 1=color 2=meta
			String[] hex = hash.Split(",");

			int iPosHex = Convert.ToInt32(hex[0] + "", 16);

			int fgColor = Convert.ToInt32(hex[1][0] + "", 16);
			int bgColor = Convert.ToInt32(hex[1][1] + "", 16);

			String str = (char)AsciiTable.table[iPosHex] + "";

			//Console.Title = $"pos: {iPosHex} , converted: {str}";
			Glyph newGlyph = new Glyph(str + "", fgColor, bgColor);

			int metaData = Convert.ToInt32(hex[2] + "", 16);
			if(metaData != 0) {
				try {
					StreamReader sr = new StreamReader(GameData.METALOCATION + metaData + ".txt");
					String catagory = sr.ReadLine().ToLower();

					if(catagory == "teleport") {
						String levelName = sr.ReadLine();
						int tpX = Convert.ToInt32(sr.ReadLine());
						int tpY = Convert.ToInt32(sr.ReadLine());

						//Teleporter tp =  new Teleporter(this, loc, Program.allLevels[levelName], new Point(tpX, tpY));

						world[loc.X, loc.Y].topObject = new Teleporter(this, loc, GameData.allLevels[levelName], new Point(tpX, tpY));
					}
					else if(catagory == "player")//224
					{
						//player = new Player(this, "Bobby", new Glyph("@", Palettes.DARK_BLUE), loc, 10);
						if(player != null) {
							player.hardSetLocation(loc);
							world[loc.X, loc.Y].topObject = player;
						}
					}
					else if(catagory == "goblin")//2
					{
						Entity gbl = new Entity(this, loc);
						Random r = new Random();
						gbl.setStats(new Glyph("G", Palettes.DARK_RED), 10, 0.25f, 10, 10, 10, 10);

						world[loc.X, loc.Y].topObject = gbl;
					}
					else if(catagory == "wall") {
						world[loc.X, loc.Y].isSolid = true;
						world[loc.X, loc.Y].isTransparent = false;
					}
					else {
					}
				}
				catch(Exception e) {
					StreamWriter sw = new StreamWriter(GameData.METALOCATION + "Error.txt");
					sw.WriteLine(e);
					sw.Close();
				}
			}

			return newGlyph;
		}

		public void update()
		{
			for(int y = 0; y < HEIGHT; y++) 
				for(int x = 0; x < WIDTH; x++) 
					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && world[x, y].topObject != null) 
						world[x, y].topObject.update();
		}

		public void render()
		{
			Point start = new Point(player.getLocation().X - viewSize / 2, player.getLocation().Y - viewSize / 2);
			Point end = new Point(player.getLocation().X + viewSize / 2, player.getLocation().Y + viewSize / 2);
			//world xy are based off player,
			for(int y = start.Y; y < end.Y; y++)
				for(int x = start.X; x < end.X; x++)
					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT)
						world[x, y].renderHidden(x + xOffset - start.X, y + yOffset - start.Y);	
			 
			//change later where point is key so lookup would be 01 and handle in main render loop 
			foreach(Point p in player.viewHandler.viewedPoints) {
				int x = p.X + xOffset - start.X, y = p.Y + yOffset - start.Y;
				if(y >= 0 && x >= 0 && x < screenSize.X && y < screenSize.Y)
					world[p.X, p.Y].render(x, y);
			}
			//render world and entites
		}
	}
}