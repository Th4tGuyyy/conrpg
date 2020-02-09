using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpgtest2020
{
	internal class VirtualConsole
	{
		public Rect bounds;

		private Queue<String> textBuffer;
		private int bufferHeight;
		private readonly int bufferWidth;

		readonly int maxConsoleHeight;

		//public ConsoleGame GameData.GAME;

		public bool READING = false;

		InputBox inputBox;

		public VirtualConsole(int x, int y, int width, int height, int inputSize = 2)
		{
			Point topLeft = new Point(x, y);
			Point bottomRight = new Point(x + width, y + height);
			bounds = new Rect(topLeft,bottomRight);

			maxConsoleHeight = height;

			bufferHeight = height - 1;
			bufferWidth = width - 1;

			textBuffer = new Queue<string>();

			inputBox = new InputBox(x, y+height- inputSize, width, inputSize);
		}


		public void render()
		{
			GameData.GAME.Engine.Frame(bounds.topLeft, bounds.bottomRight, GameData.UICOLOR);


			String[] arr = textBuffer.ToArray();
			int start = 0;
			if(READING && arr.Length == bufferHeight)//moves text if inputbox is enabled
				start = inputBox.bounds.height;

			for(int i = start; i < arr.Length; i++)
				GameData.GAME.Engine.WriteText(new Point(bounds.topLeft.X + 1, bounds.topLeft.Y + 1 + i-start), arr[i], Palettes.GRAY);


			if(READING)
				inputBox.render();
		}


		public void update()
		{

			if(READING) {
				inputBox.update();
				//writeLine(inputBox.getInput());
			}
			else if(!READING && inputBox.getInput().Length >0) {
				inputBox.update();
				if(!CommandHandler.tryCommand(inputBox.getInput()))
					writeLine(inputBox.getInput());
				inputBox.clear();
			}


		}

		public void writeLine(String text)
		{
			if(text.Length > bufferWidth) {
				//String str = text;
				writeLine(text.Substring(0, bufferWidth));
				writeLine(text[bufferWidth..]);
			}
			else {
				textBuffer.Enqueue(text);

				if(textBuffer.Count > bufferHeight)
					textBuffer.Dequeue();
			}
		}

		public void clear()
		{
			textBuffer.Clear();
		}

		public void switchState()
		{
			READING = !READING;
		}

		public void switchStateAndSlash()
		{
			READING = !READING;
			inputBox.addText("/");
		}

	}
}