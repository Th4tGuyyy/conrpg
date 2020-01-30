using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class VirtualConsole
	{
		public readonly Point topLeft; 
		public readonly Point bottomRight;

		private Queue<String> textBuffer;
		private readonly int maxHeight;
		private readonly int maxWidth;

		public ConsoleGame gameHandle;

		public VirtualConsole(int x,int y, int width, int height)
		{
			topLeft = new Point(x, y);
			bottomRight = new Point(x + width, y + height);

			maxHeight = height - 1;
			maxWidth = width-1;

			textBuffer = new Queue<string>();
		}

		public void render()
		{
			gameHandle.Engine.Frame(topLeft, bottomRight, Palettes.DARK_GRAY);


			String[] arr = textBuffer.ToArray();
			for(int i = 0; i < arr.Length; i++)
				gameHandle.Engine.WriteText(new Point(topLeft.X + 1, topLeft.Y + 1 + i), arr[i], Palettes.GRAY);
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
	}
}
