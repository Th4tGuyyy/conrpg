using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleGameEngine;
using ExtendedAscii;

namespace rpgtest2020
{
	class Level : GameData
	{
		static int xOffset = 0;
		static int yOffset = 0;
		static int viewSize = 20;
		static Point screenSize = new Point(20, 20);


		private int HEIGHT, WIDTH;

		public Tile[,] world;

		public Player player;

		public Level(String path)
		{
			loadLevel(GameData.MAPSLOCATION + path);

		}

		private void loadLevel(String path)
		{
			try {
				StreamReader sr = new StreamReader(path);

				String[] dimensions = sr.ReadLine().Split(new char[] {' '});
				WIDTH = Convert.ToInt32(dimensions[0]);
				HEIGHT = Convert.ToInt32(dimensions[1]);

				//world = new Glyph[WIDTH, HEIGHT];
				world = new Tile[WIDTH, HEIGHT];

				for(int y = 0; y < HEIGHT; y++) {
					String[] line = sr.ReadLine().Split(" ");
					for(int x=0; x< WIDTH; x++) {
						world[x, y] = new Tile();
						world[x, y].glyph = deHash(line[x], new Point(x,y));
					}
				}sr.Close();

				world[player.getLocation().X, player.getLocation().Y].topObject = player;
			}
			catch {
				
			}
		}

		private Glyph deHash(String hash, Point loc)
		{
			//0=char 1=color 2=meta
			String[] hex = hash.Split(",");
			
			int iPosHex = Convert.ToInt32(hex[0]+"", 16);

			int fgColor = Convert.ToInt32(hex[1][0]+"",16);
			int bgColor = Convert.ToInt32(hex[1][1]+"",16);


			String str = (char)AsciiTable.table[iPosHex] + "";

			//Console.Title = $"pos: {iPosHex} , converted: {str}";
			Glyph newGlyph = new Glyph(str + "" ,fgColor,bgColor);
			
			int metaData = Convert.ToInt32(hex[2]+"", 16);
			if(metaData == 1) {
				//entityList.add(loc, new Wall(this,loc));
				//world[loc.X, loc.Y].topObject = new Wall(this, loc);
				world[loc.X, loc.Y].isSolid = true;
				world[loc.X, loc.Y].isTransparent = false;


			}
			else if(metaData == 2)
			{
				Entity gbl = new Entity(this, loc);
				Random r = new Random();
				gbl.setStats(new Glyph("G", Palettes.DARK_RED), 10, 0.25f, 10, 10, 10, 10);


				world[loc.X, loc.Y].topObject = gbl;
			}
			else if(metaData == 3)
			{

				Teleporter tp = new Teleporter(this, loc,"smallworld.txt", new Point(2, 2));
				tp.sprite = new Glyph("T",Palettes.DARK_MAGENTA);
				world[loc.X, loc.Y].topObject = tp;
				/*StreamReader sr = new StreamReader(@"MetaTags/" + metaData);
				String catagory = sr.ReadLine().ToLower();

				if (catagory == "teleport")
				{
					String levelName = sr.ReadLine() + ".txt";
					int tpX = Convert.ToInt32(sr.ReadLine());
					int tpY = Convert.ToInt32(sr.ReadLine());

					Teleporter tp = new Teleporter(this, loc, levelName, new Point(tpX, tpY));

					world[loc.X, loc.Y].topObject = tp;
				}*/
			}
			else if(metaData == 224)
			{
				player = new Player(this, "Bobby", new Glyph("@", Palettes.DARK_BLUE), loc, 10);
				world[loc.X, loc.Y].topObject = player;
			}
			else
			{
				/*try
				{
					StreamReader sr = new StreamReader(@"MetaTags/" + metaData);
					String catagory = sr.ReadLine().ToLower();

					if(catagory == "teleport")
					{
						String levelName = sr.ReadLine();
						int tpX = Convert.ToInt32(sr.ReadLine());
						int tpY = Convert.ToInt32(sr.ReadLine());

						Teleporter tp = new Teleporter(this,loc,levelName,new Point(tpX,tpY));

						world[loc.X, loc.Y].topObject = tp;
					}
					else if (catagory == "player")//224
					{
						player = new Player(this, "Bobby", new Glyph("@", Palettes.DARK_BLUE), loc, 10);
						world[loc.X, loc.Y].topObject = player;
					}
					else if(catagory == "goblin")
					{
						Entity gbl = new Entity(this, loc);
						Random r = new Random();
						gbl.setStats(new Glyph("G", Palettes.DARK_RED), 10, 0.25f, 10, 10, 10, 10);


						world[loc.X, loc.Y].topObject = gbl;
					}
					else
					{

					}
				}
				catch 
				{

				}*/
			}


			return newGlyph;
		}


		public void update()
		{
			//player.update();

			//update all entities and player?

			/*Point start = new Point(player.getLocation().X - viewSize / 2, player.getLocation().Y - viewSize / 2);
			Point end = new Point(player.getLocation().X + viewSize / 2, player.getLocation().Y + viewSize / 2);

			for(int y = start.Y; y < end.Y; y++) {
				for(int x = start.X; x < end.X; x++) {

					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && world[x, y].topObject != null)
						world[x, y].topObject.update();
					//world xy are based off player,
				}
			}*/

			for(int y = 0; y < HEIGHT; y++) {
				for(int x = 0; x < WIDTH; x++) {

					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && world[x, y].topObject != null)
						world[x, y].topObject.update();
					//world xy are based off player,
				}
			}
		}


		public void render()
		{
			Point start = new Point(player.getLocation().X- viewSize/2, player.getLocation().Y-viewSize/2);
			Point end = new Point(player.getLocation().X + viewSize / 2, player.getLocation().Y + viewSize / 2);

			for(int y=start.Y;y< end.Y; y++) {
				for(int x = start.X; x < end.X; x++) {

					if(x >=0 && y>=0 && x < WIDTH && y < HEIGHT)
					{
						/*if (player.canSee(x, y))
							Glyph.setGlyph(new Point(x + xOffset - start.X, y + yOffset - start.Y), new Glyph("O", Palettes.RED, Palettes.BLACK));//world[x, y].render(x + xOffset - start.X, y + yOffset - start.Y);
						else
							world[x, y].render(x + xOffset - start.X, y + yOffset - start.Y);*/
						world[x, y].renderHidden(x + xOffset - start.X, y + yOffset - start.Y);
					}
					//world xy are based off player,
				}
			}

			foreach(Point p in player.viewHandler.viewedPoints)
			{
				int x = p.X + xOffset - start.X, y = p.Y + yOffset - start.Y;
				if (y >= 0 && x >= 0 && x < screenSize.X && y < screenSize.Y)
					world[p.X, p.Y].render(x, y);
			}
			//render world and entites
		}

		
	}

}
