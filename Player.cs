using ConsoleGameEngine;
using System;

namespace rpgtest2020
{
	internal class Player : Entity
	{
		private readonly KeyboardHandler keyHandler;
		private readonly KeyboardHandler movementKeys;

		public bool forceUpdate = false;

		public Player(Level owner, String name, Glyph sprite, Point loc, int health) : base(owner, loc)
		{
			this.name = name;
			this.location = loc;
			this.health = health;
			this.sprite = sprite;

			setStats(new Glyph("@", Palettes.DARK_CYAN), 10, 1.6f, 0, 0, 0, 0);//temp

			keyHandler = new KeyboardHandler();
			movementKeys = new KeyboardHandler();
			//moveSpeed = 1.6f;

			//Timer movement = new Timer(moveSpeed.Value);

			movementKeys.add(ConsoleKey.D, () => move(location + new Point(1, 0)), updateTimer);
			movementKeys.add(ConsoleKey.S, () => move(location + new Point(0, 1)), updateTimer);
			movementKeys.add(ConsoleKey.W, () => move(location + new Point(0, -1)), updateTimer);
			movementKeys.add(ConsoleKey.A, () => move(location + new Point(-1, 0)), updateTimer);

			keyHandler.add(ConsoleKey.B, () => say(randomNum() + ""), 0.1f);
			keyHandler.add(ConsoleKey.S, () => say("BEANS"), 3f);
		}

		public override void update()
		{
			bool playerMoved = movementKeys.handle();
			keyHandler.handle();

			if((playerMoved && lastUpdatedFrame != GAME.FrameTotal) || forceUpdate) {
				updateViewRange();
				//say(location.ToString());
				forceUpdate = false;
				lastUpdatedFrame = GAME.FrameTotal;
			}
		}

		private int randomNum()
		{
			Random r = new Random();
			return r.Next(0, 100);
		}

		public void setViewRange(int newRange)
		{
			viewHandler.setRange(newRange);
			forceUpdate = true;

		}
	}
}