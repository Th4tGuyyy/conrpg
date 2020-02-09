using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class CommandHandler : GameData
	{

		public static bool tryCommand(String command)
		{
			String[] words = command.Split(" ");
			bool happend = false;

			if(words[0] == "/tp")
				happend =teleport(words);
			if(words[0] == "/clear")
				happend = clearConsole();
			if(words[0] == "/setview")
				happend = changeRange(words);

			return happend;
		}

		private static bool changeRange(String[] words)
		{
			try {
				int newRange = Convert.ToInt32(words[1]);
				player.setViewRange(newRange);
				return true;
			}
			catch(Exception e) {
				VConsole.writeLine("Error with command: " + e);
				return false;
			}
		}

		private static bool teleport(String[] words)
		{
			try {
				int x = Convert.ToInt32(words[2]);
				int y = Convert.ToInt32(words[3]);
				Level lvl = allLevels[words[1] + ".txt"]; ;

				player.level.world[player.getLocation().X, player.getLocation().Y].topObject = null;//deletes player
				player.level = lvl;
				player.move(new Point(x,y));//moves player to targetlocation
				return true;
			}
			catch(Exception e) {
				VConsole.writeLine("Error with command: " + e);
				return false;
			}
		}

		private static bool clearConsole()
		{
			try {

				VConsole.clear();
				return true;
			}
			catch(Exception e) {
				VConsole.writeLine("Error with command: " + e);
				return false;
			}
		}
	}
}
