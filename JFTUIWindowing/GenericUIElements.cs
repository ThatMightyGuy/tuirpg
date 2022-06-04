namespace JFTUIWindowing
{
	public class Window : GenericUIObject
	{
		public int Width, Height;

		public string? Title;
		public Window(int width, int height, int x, int y, string? title, WindowStyle style, bool selectable = true, GenericUIObject? parent = null)
		{
			Width = width;
			Height = height;
			X = x;
			Y = y;
			if (parent is not null)
				parent.AddChild(this);
			Parent = parent;
			Title = title;
			Style = style;
			Selectable = selectable;
		}
		private static void Draw(int width, int height, string? title, WindowStyle style, bool selected, bool drawBackground = true)
		{
			int offsetLeft = Console.CursorLeft;
			ConsoleColor oldBG = Console.BackgroundColor;
			ConsoleColor oldFG = Console.ForegroundColor;
			Console.BackgroundColor = style.BackgroundColor;
			bool drawTitle = title is not null;
			string titleString = "";
			if (drawTitle)
				titleString = style.LeftTitleSeparator + new string(' ', style.TitleMargin) + title + new string(' ', style.TitleMargin) + style.RightTitleSeparator;
			for (int y = 0; y < height; y++)
			{
				if (selected)
					Console.ForegroundColor = style.SelectedBorder;
				else
					Console.ForegroundColor = style.Border;
				for (int x = 0; x < width; x++)
				{
					try
					{
						if (x == 0 && y >= style.TitlePaddingTL && style.VerticalTitle && drawTitle)
							Console.Write(titleString[y - style.TitlePaddingTL]);
						else if (y == 0 && x >= style.TitlePaddingTL && !style.VerticalTitle && drawTitle)
							Console.Write(titleString[x - style.TitlePaddingTL]);
						else if (y == x && y == 0)
							Console.Write(style.BorderCorners.Top);
						else if (y == height - 1 && x == width - 1)
							Console.Write(style.BorderCorners.Bottom);
						else if (y == 0 && x == width - 1)
							Console.Write(style.BorderCorners.Left);
						else if (y == height - 1 && x == 0)
							Console.Write(style.BorderCorners.Right);
						else if (y == 0)
							Console.Write(style.BorderSides.Left);
						else if (y == height - 1)
							Console.Write(style.BorderSides.Right);
						else if (x == 0)
							Console.Write(style.BorderSides.Top);
						else if (x == width - 1)
							Console.Write(style.BorderSides.Bottom);
						else if (drawBackground)
							Console.Write(' ');
						else
							Console.CursorLeft++;
					}
					catch (IndexOutOfRangeException)
					{
						drawTitle = false;
						if (style.VerticalTitle) y--; else x--;
					}
				}
				Console.CursorTop++;
				Console.CursorLeft = offsetLeft;
			}
			Console.BackgroundColor = oldBG;
			Console.ForegroundColor = oldFG;
		}
		public void Create(bool drawBackground = true)
		{
			if (Parent is null)
				Console.SetCursorPosition(X, Y);
			else
				Console.SetCursorPosition(Parent.X + Parent.Style.Margin.Left + X, Parent.Y + Parent.Style.Margin.Top + Y);
			Draw(Width, Height, Title, Style, Selected, drawBackground);
			//Console.SetCursorPosition(curX, curY); // disabled for testing, maybe actually unnecessary
		}
		internal override void Update()
		{
			foreach (GenericUIObject obj in Children)
				try
				{
					obj.Update();
				}
				catch (NotImplementedException)
				{
					continue;
				}
			Create(false);
		}
	}
	public class Checkbox : GenericUIObject
	{
		public bool State;
		public string? Text;
		public Checkbox(int x, int y, bool state, string? text, WindowStyle style, bool selectable = true, GenericUIObject? parent = null)
		{
			Text = text;
			State = state;
			Style = style;
			if (parent is not null)
				parent.AddChild(this);
			Parent = parent;
			X = x;
			Y = y;
			Selectable = selectable;
		}
		public bool Toggle()
		{
			State = !State;
			Update();
			return State;
		}

		private static void Draw(bool state, string? text, WindowStyle style, bool selected)
		{
			ConsoleColor oldBG = Console.BackgroundColor;
			ConsoleColor oldFG = Console.ForegroundColor;
			Console.ForegroundColor = selected ? style.ForegroundColorAccent : style.ForegroundColor;
			Console.BackgroundColor = style.BackgroundColor;
			Console.Write($"{style.SmallButtonLeft}{(state ? style.CheckboxChecked :style.CheckboxUnchecked)}{style.SmallButtonRight} {(text is null ? "" : text)}");
			Console.ForegroundColor = oldFG;
			Console.BackgroundColor = oldBG;
		}

		public void Create()
		{
			if (Parent is null)
				Console.SetCursorPosition(X, Y);
			else
				Console.SetCursorPosition(Parent.X + Parent.Style.Margin.Left + X, Parent.Y + Parent.Style.Margin.Top + Y);
			Draw(State, Text, Style, Selected);
		}
		internal override void Update()
		{
			Create();
		}
		internal override void Click()
		{
			Toggle();
		}
	}
	public class Label : GenericUIObject
	{
		public string Text;
		public Label(string text, WindowStyle style)
		{
			Text = text;
			Style = style;
		}
	}
	public class Button : GenericUIObject
	{
		public string Text;
		public int Width, Height;
		public Button(int width, int height, int x, int y, string text, WindowStyle style, bool selectable = true, GenericUIObject? parent = null)
		{
			Text = text;
			Style = style;
			if (parent is not null)
				parent.AddChild(this);
			Parent = parent;
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Selectable = selectable;
		}

		private static void Draw(string text, int width, int height, WindowStyle style, bool selected)
		{
			ConsoleColor oldBG = Console.BackgroundColor;
			ConsoleColor oldFG = Console.ForegroundColor;
			Console.ForegroundColor = selected ? style.ForegroundColorAccent : style.ForegroundColor;
			Console.BackgroundColor = style.BackgroundColor;
			if (height <= 1)
			{
				Console.Write($"{style.SmallButtonLeft}{text}{style.SmallButtonRight}");
			}
			else
			{
				int offsetLeft = Console.CursorLeft;
				int offsetTop = Console.CursorTop;
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						if (y == x && y == 0)
							Console.Write(style.LargeButtonCorners.Top);
						else if (y == height - 1 && x == width - 1)
							Console.Write(style.LargeButtonCorners.Bottom);
						else if (y == 0 && x == width - 1)
							Console.Write(style.LargeButtonCorners.Left);
						else if (y == height - 1 && x == 0)
							Console.Write(style.LargeButtonCorners.Right);
						else if (y == 0)
							Console.Write(style.LargeButtonSides.Left);
						else if (y == height - 1)
							Console.Write(style.LargeButtonSides.Right);
						else if (x == 0)
							Console.Write(style.LargeButtonSides.Top);
						else if (x == width - 1)
							Console.Write(style.LargeButtonSides.Bottom);
						else
							Console.Write(' ');
					}
					Console.CursorTop++;
					Console.CursorLeft = offsetLeft;
				}
				Console.CursorTop = offsetTop + height / 2;
				Console.CursorLeft = offsetLeft + width / 2 - text.Length / 2;
				Console.Write(text);
			}
			Console.ForegroundColor = oldFG;
			Console.BackgroundColor = oldBG;
		}
		
		public void Create()
		{
			if (Parent is null)
				Console.SetCursorPosition(X, Y);
			else
				Console.SetCursorPosition(Parent.X + Parent.Style.Margin.Left + X, Parent.Y + Parent.Style.Margin.Top + Y);
			Draw(Text, Width, Height, Style, Selected);
		}
		internal override void Update()
		{
			Create();
		}
		internal override void Click()
		{

		}
	}
	public class ButtonArgs : EventArgs
	{
		public bool State;
		public ButtonArgs(bool state) => State = state;
	}
}
