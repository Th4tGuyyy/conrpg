using ConsoleGameEngine;
using ExtendedAscii;
using System;
using System.IO;

namespace rpgtest2020
{
	internal class Level : GameData
	{
		public static readonly Rect bounds = new Rect(levelXOffset-1,levelYOffset-1,levelViewSize+ levelXOffset + 1, levelViewSize+levelYOffset + 1);

		private int HEIGHT, WIDTH;

		public Tile[,] world;

		private String path;

		public Level(String path)
		{
			this.path = GameData.MAPSLOCATION + path;
		}

		public void loadLevel()//time reminant might be caused if mutliple player spawns?
		{
			StreamReader sr = new StreamReader(path);

			String[] dimensions = sr.ReadLine().Split(new char[] { ' ' });
			WIDTH = Convert.ToInt32(dimensions[0]);
			HEIGHT = Convert.ToInt32(dimensions[1]);

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

		private Glyph deHash(String hash, Point loc)
		{
			//0=char 1=color 2=meta
			String[] hex = hash.Split(",");

			//converts hex to info
			int iPosHex = Convert.ToInt32(hex[0] + "", 16);

			int fgColor = Convert.ToInt32(hex[1][0] + "", 16);
			int bgColor = Convert.ToInt32(hex[1][1] + "", 16);

			String str = (char)AsciiTable.table[iPosHex] + "";

			//creats glyph
			Glyph newGlyph = new Glyph(str + "", fgColor, bgColor);

			int metaData = Convert.ToInt32(hex[2] + "", 16);
			if(metaData != 0) {
				try {
					StreamReader sr = new StreamReader(METALOCATION + metaData + ".txt");
					String catagory = sr.ReadLine().ToLower();

					if(catagory == "teleport") {
						String levelName = sr.ReadLine();
						int tpX = Convert.ToInt32(sr.ReadLine());
						int tpY = Convert.ToInt32(sr.ReadLine());

						world[loc.X, loc.Y].topObject = new Teleporter(this, loc, GameData.allLevels[levelName], new Point(tpX, tpY));
					}
					else if(catagory == "player")//224
					{
						if(player.level == this) {
							player.hardSetLocation(loc);
							world[loc.X, loc.Y].topObject = player;
						}
					}
					else if(catagory == "goblin")//2
					{
						Entity gbl = new Entity(this, loc);
						//Random r = new Random();
						gbl.setStats(new Glyph("G", Palettes.DARK_RED), 10, 2f, 10, 10, 10, 10);

						world[loc.X, loc.Y].topObject = gbl;
					}
					else if(catagory == "wall") {
						world[loc.X, loc.Y].isSolid = true;
						world[loc.X, loc.Y].isTransparent = false;
					}
					else {
						throw new Exception("meta data went fucky wucky: " + metaData);
					}
					sr.Close();
				}
				catch(Exception e) {
					StreamWriter sw = new StreamWriter(GameData.METALOCATION + "Error.txt");
					sw.WriteLine(e);
					sw.WriteLine(e.InnerException);
					sw.Close();
				}
			}

			return newGlyph;
		}

		public void update()
		{
			for(int y = 0; y < HEIGHT; y++)
				for(int x = 0; x < WIDTH; x++)
					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT)
						world[x, y].tryUpdate();
		}

		public void render()
		{
			GAME.Engine.Frame(new Point(levelXOffset - 1, levelYOffset - 1), new Point(levelViewSize + 1, levelViewSize + 1), UICOLOR);//frame around game

			Point start = new Point(player.getLocation().X - levelViewSize / 2, player.getLocation().Y - levelViewSize / 2);
			Point end = new Point(player.getLocation().X + levelViewSize / 2, player.getLocation().Y + levelViewSize / 2);
			//world xy are based off player,
			for(int y = start.Y; y < end.Y + 1; y++)
				for(int x = start.X; x < end.X + 1; x++)
					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT)
						world[x, y].renderHidden(x + levelXOffset - start.X, y + levelYOffset - start.Y);

			//change later where point is key so lookup would be 01 and handle in main render loop
			foreach(Point p in player.getLastViewPoints()/*player.viewHandler.viewedPoints*/) {
				int x = p.X + levelXOffset - start.X, y = p.Y + levelYOffset - start.Y;
				if(y > 0 && x > 0 && x < levelViewSize && y < levelViewSize)
					world[p.X, p.Y].render(x, y);
			}
			//render world and entites
		}
	}
}