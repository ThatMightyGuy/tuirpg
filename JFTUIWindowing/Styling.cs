namespace JFTUIWindowing
{	
	public struct WindowStyle
	{
		public Spacing Margin;
		public WindowFrame BorderSides;
		public WindowFrame BorderCorners;
		public ConsoleColor BackgroundColor, ForegroundColor, ForegroundColorAccent, Border, SelectedBorder, SelectedText;
		public bool VerticalTitle;
		public char? LeftTitleSeparator, RightTitleSeparator;
		public int TitlePaddingTL, TitleMargin;
		public char? SmallButtonLeft, SmallButtonRight;
		public WindowFrame LargeButtonSides, LargeButtonCorners;
		public int LargeButtonMargin;
		public char CheckboxUnchecked, CheckboxChecked;
		public WindowStyle(Spacing margin, WindowFrame borderSides, WindowFrame borderCorners,
			ConsoleColor backgroundColor, ConsoleColor foregroundColor, ConsoleColor foregroundColorAccent, ConsoleColor border, ConsoleColor selectedBorder, ConsoleColor selectedText, 
			bool verticalTitle, char leftTitleSeparator, char rightTitleSeparator, int titlePaddingTL, int titleMargin,
			char? smallButtonLeft, char? smallButtonRight, WindowFrame largeButtonSides, WindowFrame largeButtonCorners, int largeButtonMargin,
			char checkboxUnchecked, char checkboxChecked)
		{
			Margin = margin;
			BorderSides = borderSides;
			BorderCorners = borderCorners;
			BackgroundColor = backgroundColor;
			ForegroundColor = foregroundColor;
			ForegroundColorAccent = foregroundColorAccent;
			Border = border;
			SelectedBorder = selectedBorder;
			SelectedText = selectedText;
			VerticalTitle = verticalTitle;
			LeftTitleSeparator = leftTitleSeparator;
			RightTitleSeparator = rightTitleSeparator;
			TitlePaddingTL = titlePaddingTL;
			TitleMargin = titleMargin;
			SmallButtonLeft = smallButtonLeft;
			SmallButtonRight = smallButtonRight;
			LargeButtonSides = largeButtonSides;
			LargeButtonCorners = largeButtonCorners;
			LargeButtonMargin = largeButtonMargin;
			CheckboxUnchecked = checkboxUnchecked;
			CheckboxChecked = checkboxChecked;
		}
	}

	public struct Spacing
	{
		public int Left, Right, Top, Bottom;
		public Spacing(int all) => Left = Right = Top = Bottom = all;
		public Spacing(int vertical, int horizontal)
		{
			Left = Right = horizontal;
			Top = Bottom = vertical;
		}
		public Spacing(int top, int left, int bottom, int right)
		{
			Top = top;
			Left = left;
			Right = right;
			Bottom = bottom;
		}
	}
	public struct WindowFrame
	{
		public char Left, Right, Top, Bottom;
		public WindowFrame(char vertical, char horizontal)
		{
			Left = Right = horizontal;
			Top = Bottom = vertical;
		}
		public WindowFrame(char top, char left, char bottom, char right)
		{
			Top = top;
			Left = left;
			Right = right;
			Bottom = bottom;
		}
	}
}
