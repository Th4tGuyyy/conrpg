using ConsoleGameEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpgtest2020
{
	internal class VirtualConsole
	{
		public readonly Rect bounds;

		private Queue<String> textBuffer;
		private readonly int maxHeight;
		private readonly int maxWidth;

		//public ConsoleGame GameData.GAME;

		public bool READING = false;

		InputBox inputBox;
		bool enableInput = true;

		public VirtualConsole(int x, int y, int width, int height, int inputSize = 2)
		{
			Point topLeft = new Point(x, y);
			Point bottomRight = new Point(x + width, y + height- inputSize);
			bounds = new Rect(topLeft,bottomRight);

			maxHeight = height - 1- inputSize;
			maxWidth = width - 1- inputSize;

			textBuffer = new Queue<string>();

			inputBox = new InputBox(x, y+height- inputSize, width, inputSize);
		}


		public void render()
		{
			GameData.GAME.Engine.Frame(bounds.topLeft, bounds.bottomRight, GameData.UICOLOR);

			if(enableInput)
				inputBox.render();



			String[] arr = textBuffer.ToArray();
			for(int i = 0; i < arr.Length; i++)
				GameData.GAME.Engine.WriteText(new Point(bounds.topLeft.X + 1, bounds.topLeft.Y + 1 + i), arr[i], Palettes.GRAY);
		}

		public void update()
		{
			if(enableInput)
				inputBox.update();
		}

		public void writeLine(String text)
		{
			if(text.Length > maxWidth) {
				//String str = text;
				writeLine(text.Substring(0, maxWidth));
				writeLine(text[maxWidth..]);
			}
			else {
				textBuffer.Enqueue(text);

				if(textBuffer.Count > maxHeight)
					textBuffer.Dequeue();
			}
		}

		public void switchState()
		{
			READING = !READING;
		}

	}
}