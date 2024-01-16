namespace Pendu.Services.WordProviders
{
	public class OneWordProvider(string uniqueWord = "PENDU") : IWordProvider
	{
		private readonly string onlyWord = uniqueWord.ToUpper();

		public string GetWord()
		{
			return this.onlyWord;
		}
	}
}
