using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class KeyboardHandler : GameData
	{
		private class Node
		{
			public ConsoleKey key;
			public Action action;
			public Timer timer;

			public Node(ConsoleKey key, Action action, Timer timer)
			{
				this.key = key;
				this.action = action;
				this.timer = timer;
			}
		}

		private List<Node> keyList = new List<Node>();

		public void add(ConsoleKey key, Action action, Timer timer)
		{
			keyList.Add(new Node(key, action, timer));
		}
		public void add(ConsoleKey key, Action action, float cooldown)
		{
			add(key, action, new Timer(cooldown));
		}

		public bool handle()
		{
			bool action = false;

			for(int i =0; i < keyList.Count; i++) {


				if(GAME.Engine.GetKey(keyList[i].key) && keyList[i].timer.complete()) {
					keyList[i].timer.start();
					keyList[i].action();
					action = true;
				}

				keyList[i].timer.update();


			}

			return action;
		}
			
	}
}
