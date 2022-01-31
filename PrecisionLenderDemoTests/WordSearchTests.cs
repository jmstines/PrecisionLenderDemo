using NUnit.Framework;
using System.Linq;

namespace PrecisionLenderDemo.Tests
{
	class WordSearchTests
	{
		public string[] WordDictionary = { "good", "bad", "dog", "cat", "do", "dont" };


		[Test]
		public void GiveWordDictionary_FindsCorrectMatches()
		{
			var search = new WordSearch(WordDictionary);
			var input = "ddelgoo";
			var values = new string[] { "good", "dog", "do" };

			var matches = search.SearchString(input);

			Assert.AreEqual(matches, values);
		}


		[Test]
		public void GivenWordDictionaryAndNoMatchs_ReturnEmptyList()
		{
			var search = new WordSearch(WordDictionary);
			var input = "asdf";
			var values = new string[] { };

			var matches = search.SearchString(input);

			Assert.AreEqual(matches, values);
		}
	}
}
