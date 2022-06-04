namespace JFConfigReader
{
	public static class ConfigTools
	{
		public static Dictionary<string, string> Read(string path)
		{
			Dictionary<string, string> keyValuePairs = new();
			string[] lines = File.ReadAllLines(path);
			foreach(string line in lines)
			{
				if (line.StartsWith('#')) continue;
				KeyValuePair<string, string> kvp = Split(line);
				keyValuePairs.Add(kvp.Key, kvp.Value);
			}
			return keyValuePairs;
		}
		private static KeyValuePair<string, string> Split(string line)
		{
			bool left = true;
			string leftStr = "", rightStr = "";
			bool isInString = false;
			for(int i = 0; i < line.Length; i++)
				if (line[i] == '=' && left)
					left = false;
				else if (line[i] == '#' && !isInString)
					break;
				else if (line[i] == '"')
					if (isInString)
						if (line[i - 1] == '\\')
							rightStr += line[i];
						else
							isInString = false;
					else
						isInString = true;
				else
					if (left)
						leftStr += line[i];
					else
						rightStr += line[i];
			return new KeyValuePair<string, string>(leftStr, rightStr);
		}
	}
}