using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine;

namespace rpgtest2020
{
	class InputBox
	{
		public Rect bounds;

		float inputDelay = 0.005f;
		String input = "";//change to string builder
		int maxWidth;

		KeyboardHandler keys = new KeyboardHandler();


		public InputBox(int x, int y, int width, int height)
		{
			bounds = new Rect(new Point(x, y), new Point(x + width, y + height));
			maxWidth = width - 1;

			#region QWERTY
			//sorry father, for i have sinned
			keys.add(ConsoleKey.Q, () => handleKey("q"), inputDelay,true);
			keys.add(ConsoleKey.W, () => handleKey("w"), inputDelay,true);
			keys.add(ConsoleKey.E, () => handleKey("e"), inputDelay,true);
			keys.add(ConsoleKey.R, () => handleKey("r"), inputDelay,true);
			keys.add(ConsoleKey.T, () => handleKey("t"), inputDelay,true);
			keys.add(ConsoleKey.Y, () => handleKey("y"), inputDelay,true);
			keys.add(ConsoleKey.U, () => handleKey("u"), inputDelay,true);
			keys.add(ConsoleKey.I, () => handleKey("i"), inputDelay,true);
			keys.add(ConsoleKey.O, () => handleKey("o"), inputDelay,true);
			keys.add(ConsoleKey.P, () => handleKey("p"), inputDelay,true);
			keys.add(ConsoleKey.A, () => handleKey("a"), inputDelay,true);
			keys.add(ConsoleKey.S, () => handleKey("s"), inputDelay,true);
			keys.add(ConsoleKey.D, () => handleKey("d"), inputDelay,true);
			keys.add(ConsoleKey.F, () => handleKey("f"), inputDelay,true);
			keys.add(ConsoleKey.G, () => handleKey("g"), inputDelay,true);
			keys.add(ConsoleKey.H, () => handleKey("h"), inputDelay,true);
			keys.add(ConsoleKey.J, () => handleKey("j"), inputDelay,true);
			keys.add(ConsoleKey.K, () => handleKey("k"), inputDelay,true);
			keys.add(ConsoleKey.L, () => handleKey("l"), inputDelay,true);
			keys.add(ConsoleKey.Z, () => handleKey("z"), inputDelay,true);
			keys.add(ConsoleKey.X, () => handleKey("x"), inputDelay,true);
			keys.add(ConsoleKey.C, () => handleKey("c"), inputDelay,true);
			keys.add(ConsoleKey.V, () => handleKey("v"), inputDelay,true);
			keys.add(ConsoleKey.B, () => handleKey("b"), inputDelay,true);
			keys.add(ConsoleKey.N, () => handleKey("n"), inputDelay,true);
			keys.add(ConsoleKey.M, () => handleKey("m"), inputDelay,true);

			keys.add(ConsoleKey.D0, () => handleKey("0"), inputDelay, true);
			keys.add(ConsoleKey.D1, () => handleKey("1"), inputDelay, true);
			keys.add(ConsoleKey.D2, () => handleKey("2"), inputDelay, true);
			keys.add(ConsoleKey.D3, () => handleKey("3"), inputDelay, true);
			keys.add(ConsoleKey.D4, () => handleKey("4"), inputDelay, true);
			keys.add(ConsoleKey.D5, () => handleKey("5"), inputDelay, true);
			keys.add(ConsoleKey.D6, () => handleKey("6"), inputDelay, true);
			keys.add(ConsoleKey.D7, () => handleKey("7"), inputDelay, true);
			keys.add(ConsoleKey.D8, () => handleKey("8"), inputDelay, true);
			keys.add(ConsoleKey.D9, () => handleKey("9"), inputDelay, true);
			#endregion

			keys.add(0xBF,() => handleKey("/"), inputDelay, true);
			keys.add(ConsoleKey.Spacebar,() => handleKey(" "),inputDelay,true);
			keys.add(ConsoleKey.Backspace, () => handleKey("bks"), inputDelay,true);
		}

		public void render()
		{
			GameData.GAME.Engine.Frame(bounds.topLeft, bounds.bottomRight, GameData.UICOLOR);

			int startPos = input.Length - maxWidth;
			if(startPos < 0)
				startPos = 0;

			GameData.GAME.Engine.WriteText(bounds.topLeft +1, input[startPos..], GameData.UICOLOR);
		}

		public void update()
		{
			keys.handle();
			
		}

		public void handleKey(String key)
		{
			
			if(key == "bks") {
				if(input.Length > 0)
					input = input.Substring(0, input.Length - 1);
			}
			else {
				input += key;
			}
		}

		public String getInput()
		{
			return input;
		}

		public void clear()
		{
			input = "";
		}

		public void addText(String text)
		{
			input += text;
		}

	}
}
