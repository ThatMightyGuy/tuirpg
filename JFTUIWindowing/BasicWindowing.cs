namespace JFTUIWindowing
{
	[Flags]
	public enum Sides
	{
		Left = 1,
		Right = 2,
		Top = 4,
		Bottom = 8
	}

	public class GenericUIObject
	{
		public int X, Y;
		public bool Enable;
		public bool Selectable;
		private bool _selected;
		public bool Selected
		{
			get => _selected;
			set
			{
				Update();
				ObjectSelectedEvent(this, new ObjectSelectedArgs(value));
			}
		}
		public int Anchor;
		public GenericUIObject? Parent;
		public List<GenericUIObject> Children;
		public WindowStyle Style;


		internal virtual void Update() => throw new NotImplementedException("This object does not implement updating");
		internal virtual void Click() => Clicked(this);
		public GenericUIObject()
		{
			X = 0;
			Y = 0;
			Anchor = 3;
			Enable = false;
			Parent = null;
			Children = new();
			Selected = false;
		}
		public GenericUIObject(int x, int y, int anchor = 3, bool enable = true, GenericUIObject? parent = null, bool selectable = true, bool selected = false)
		{
			X = x;
			Y = y;
			Anchor = anchor;
			Enable = enable;
			Parent = null;
			Children = new();
			if (parent is not null)
				parent.AddChild(this);
			Selected = selected;
			Selectable = selectable;
		}

		public delegate void ClickedEventHandler(object sender);
		public event ClickedEventHandler Clicked;

		public void AddChild(GenericUIObject uiObject) => Children.Add(uiObject);

		public delegate void ObjectSelectedEventHandler(object sender, ObjectSelectedArgs args);
		public event ObjectSelectedEventHandler ObjectSelected;
		public class ObjectSelectedArgs : EventArgs
		{
			public bool Selected;
			public ObjectSelectedArgs(bool selected) => Selected = selected;
		}
		private void ObjectSelectedEvent(object source, ObjectSelectedArgs args)
		{
			_selected = args.Selected;
			if (source is null) return;
			if(args.Selected)
				switch(Console.ReadKey().Key)
				{
					case ConsoleKey.RightArrow:
						if (Parent is not null)
						{
							Selected = false;
							try
							{
								Parent.Children[Parent.Children.IndexOf(this) + 1].Selected = true;
							}
							catch(ArgumentOutOfRangeException)
							{
								Parent.Children[0].Selected = true;
							}
							Update();
						}
						break;
					case ConsoleKey.LeftArrow:
						if (Parent is not null)
						{
							Selected = false;
							try
							{
								Parent.Children[Parent.Children.IndexOf(this) - 1].Selected = true;
							}
							catch (ArgumentOutOfRangeException)
							{
								Parent.Children[^1].Selected = true;
							}
							Update();
						}
						break;
					case ConsoleKey.Escape:
						if (Parent is not null)
						{
							Selected = false;
							Parent.Selected = true;
							Update();
						}
						break;
					case ConsoleKey.Enter:
						if (Children.Count > 0)
						{
							for (int i = 0; i < Children.Count; i++)
								if (Children[i].Selectable)
								{
									Selected = false;
									Children[i].Selected = true;
								}
							Update();
						}
						break;
					case ConsoleKey.Spacebar:
						if (Clicked is not null)
							Click();
						break;
					
				}
			if (ObjectSelected is not null)
				ObjectSelected(this, args);
		}
	}
	
}