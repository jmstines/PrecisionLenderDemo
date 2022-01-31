using System.Collections.Generic;

namespace PrecisionLenderDemo
{
	public class WordSearch
	{
		private readonly string[] Words;

		public WordSearch(string[] words)
		{
			Words = words;
		}

		public string[] SearchString(string characters)
		{
			var wordsFound = new List<string>();
			for (int i = 0; i < Words.Length; i++)
			{
				if (IsMatch(characters, Words[i]))
				{
					wordsFound.Add(Words[i]);
				}
			}

			return wordsFound.ToArray();
		}

		public bool IsMatch(string characters, string word)
		{
			if(word.Length == 0)
			{
				return true;
			}

			var match = false;

			for (int i = 0; i < word.Length; i++)
			{
				for (int j = 0; j < characters.Length; j++)
				{
					if(word[i] == characters[j])
					{
						var subWord = word.Remove(i, 1);
						var subCharacters = characters.Remove(j, 1);
						if(IsMatch(subCharacters, subWord))
						{
							match = true;
							break;
						}
					}
				}
			}

			return match;
		}
	}
}
