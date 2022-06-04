using JFTUIWindowing;
using static JFTUIWindowing.GenericUIObject;
namespace tuirpg
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Console.Clear();
			WindowStyle windowStyle = new(new Spacing(1), new WindowFrame('║', '═'), new WindowFrame('╔', '╗', '╝', '╚'),
				ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.Cyan,
				false, '╡', '╞', 15, 1,
				'[', ']', new WindowFrame('│', '─'), new WindowFrame('┌', '┐', '┘', '└'), 1,
				' ', 'V');
			Window window = new(Console.WindowWidth, Console.WindowHeight - 1, 0, 0, "Test Window", windowStyle);
			Window window2 = new(50, 10, 0, 0, "Test Window 2", windowStyle, true, window);

			Checkbox checkboxOn = new(1, 1, true, "Test Checkbox - On", windowStyle, true, window2);
			Checkbox checkboxOff = new(1, 2, false, "Test Checkbox - Off", windowStyle, true, window2);

			Button buttonLarge = new(25, 3, 30, 14, "Large Button", windowStyle, true, window);
			Button buttonSmall = new(16, 1, 1, 3, "Small Button", windowStyle, true, window2);

			
			window.Create();
			window2.Create();
			
			checkboxOn.Create();
			checkboxOff.Create();

			buttonLarge.Create();
			buttonSmall.Create();
			window.Selected = true;


			window.ObjectSelected += Window_ObjectSelected;

			//Console.Read();
		}
		private static void Window_ObjectSelected(object source, ObjectSelectedArgs args)
		{
		}
	}
}
