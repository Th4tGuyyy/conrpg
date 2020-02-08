using System;
using System.Collections.Generic;

namespace rpgtest2020
{
	internal class KeyboardHandler : GameData
	{
		private class keyNode
		{
			public ConsoleKey key;
			public Action action;
			public Timer timer;
			public bool keyDown;

			public keyNode next;

			public keyNode(ConsoleKey key, Action action, Timer timer,bool keydown)
			{
				this.key = key;
				this.action = action;
				this.timer = timer;
				this.keyDown = keydown;
			}
		}

		private keyNode top;

		public void add(ConsoleKey key, Action action, Timer timer, bool keydown = false)
		{
			keyNode newNode = new keyNode(key, action, timer,keydown);
			newNode.next = top;
			top = newNode;
		}

		public void add(ConsoleKey key, Action action, float cooldown, bool keydown = false)
		{
			add(key, action, new Timer(cooldown),keydown);
		}

		/// <summary>loops thru the keys and checks logic if pressed, if so do its action</summary>
		/// <returns>returns if ANY action has occured from a keypress</returns>
		public bool handle()
		{
			bool action = false;

			keyNode cur = top;
			while(cur != null) {
				bool pressed;

				if(!cur.keyDown)
					pressed = GAME.Engine.GetKey(cur.key);
				else
					pressed = GAME.Engine.GetKeyDown(cur.key);


				if(pressed && cur.timer.complete()) {
					cur.timer.start();
					cur.action();
					action = true;
				}

				cur.timer.update();

				cur = cur.next;
			}

			return action;
		}
	}
}