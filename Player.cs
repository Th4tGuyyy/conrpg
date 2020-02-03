using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class Player : Entity
	{
		readonly KeyboardHandler keyHandler;

		public Player(Level owner,String name,Glyph sprite, Point loc, int health): base(owner,loc)
		{
			this.name = name;
			this.location = loc;
			this.health = health;
			this.sprite = sprite;

			setStats(new Glyph("@",Palettes.DARK_CYAN),10,1f,0,0,0,0);//temp

			keyHandler = new KeyboardHandler();
			//moveSpeed = 1.6f;

			//Timer movement = new Timer(moveSpeed.Value);

			keyHandler.add(ConsoleKey.D, () => move(location + new Point(1, 0)), updateTimer);
			keyHandler.add(ConsoleKey.S, () => move(location + new Point(0, 1)), updateTimer);
			keyHandler.add(ConsoleKey.W, () => move(location + new Point(0, -1)), updateTimer);
			keyHandler.add(ConsoleKey.A, () => move(location + new Point(-1, 0)), updateTimer);

			keyHandler.add(ConsoleKey.B, () => say("HELLO!"), 0.1f);
			keyHandler.add(ConsoleKey.S, () => say("BEANS"), 3f);

		}

		public override void update()
		{
			bool playerMoved = keyHandler.handle();

			if (playerMoved)
			{
				updateViewRange();
			}
		}


	}
}
