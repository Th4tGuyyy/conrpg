﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleGameEngine;
using ExtendedAscii;

namespace rpgtest2020
{
	class Level //: Entity
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
			loadLevel(path);
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
				//world[loc.X, loc.Y].entity = new Wall(this, loc);
				world[loc.X, loc.Y].isSolid = true;


			}
			else if(metaData == 2) {
				Goblin gbl = new Goblin(this, loc);
				Random r = new Random();
				gbl.setStats(new Glyph("G", Palettes.DARK_RED), 10, 0.25f, 10, 10, 10, 10);


				world[loc.X, loc.Y].entity = gbl;
				
			}


			return newGlyph;
		}


		public void update()
		{
			player.update();

			//update all entities and player?

			/*Point start = new Point(player.getLocation().X - viewSize / 2, player.getLocation().Y - viewSize / 2);
			Point end = new Point(player.getLocation().X + viewSize / 2, player.getLocation().Y + viewSize / 2);

			for(int y = start.Y; y < end.Y; y++) {
				for(int x = start.X; x < end.X; x++) {

					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && world[x, y].entity != null)
						world[x, y].entity.update();
					//world xy are based off player,
				}
			}*/

			for(int y = 0; y < HEIGHT; y++) {
				for(int x = 0; x < WIDTH; x++) {

					if(x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && world[x, y].entity != null)
						world[x, y].entity.update();
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
						world[x,y].render(x+xOffset- start.X, y+yOffset- start.Y);
					//world xy are based off player,
				}
			}
			//render world and entites
		}

		
	}

}
