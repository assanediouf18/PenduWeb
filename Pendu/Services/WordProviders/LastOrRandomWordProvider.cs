
namespace Pendu.Services.WordProviders
{
	public class LastOrRandomWordProvider : WordFromFileProvider
	{
		public LastOrRandomWordProvider(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
		{
		}

		public string? savedWord;

		public void SetSavedWord(string? savedWord)
		{
			this.savedWord = savedWord;
		}

		public string GetWord()
		{
			if(!String.IsNullOrEmpty(savedWord))
			{
				return savedWord;
			}
			return base.GetWord();
		}
	}
}
