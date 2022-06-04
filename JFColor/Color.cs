namespace JFColor
{
	public static class Color
	{
		private static readonly Dictionary<char, ConsoleColor> ConsoleColors = new()
		{
			{'r', ConsoleColor.DarkRed},
			{'g', ConsoleColor.DarkGreen},
			{'b', ConsoleColor.DarkBlue},
			{'R', ConsoleColor.Red},
			{'G', ConsoleColor.Green},
			{'B', ConsoleColor.Blue},
			{'c', ConsoleColor.DarkCyan},
			{'m', ConsoleColor.DarkMagenta},
			{'y', ConsoleColor.DarkYellow},
			{'C', ConsoleColor.Cyan},
			{'M', ConsoleColor.Magenta},
			{'Y', ConsoleColor.Yellow},
			{'k', ConsoleColor.Black},
			{'K', ConsoleColor.DarkGray},
			{'w', ConsoleColor.Gray},
			{'W', ConsoleColor.White}
		};
		public static bool Write(string value)
		{
			bool flawless = true;
			for(int i = 0; i < value.Length; i++)
			{
				try
				{
					switch (value[i])
					{
						case '%':
							Console.BackgroundColor = ConsoleColors[value[++i]];
							break;
						case '&':
							Console.ForegroundColor = ConsoleColors[value[++i]];
							break;
						default:
							Console.Write(value[i]);
							break;
					}
				}
				catch(IndexOutOfRangeException)
				{
					flawless = false;
					break;
				}
				catch (KeyNotFoundException)
				{
					Console.Write("&" + value[i]);
					flawless = false;
				}
			}
			return flawless;
		}
		public static bool WriteLine(string value, bool ignoreColumn = false)
		{
			if(!ignoreColumn)
				Console.CursorLeft = 0;
			return Write(value + Environment.NewLine);
		}
	}
}