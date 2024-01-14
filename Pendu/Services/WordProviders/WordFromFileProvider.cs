namespace Pendu.Services.WordProviders
{
	public class WordFromFileProvider : IWordProvider
	{
		public WordFromFileProvider(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
		}

		public IWebHostEnvironment WebHostEnvironment { get; }

		private String WordsFileName
		{
			get
			{
				return Path.Combine(WebHostEnvironment.WebRootPath, "data", "words.txt");
			}
		}

		public string GetWord()
		{
			if (File.Exists(WordsFileName))
			{
				String[] words = File.ReadAllLines(WordsFileName);
				Random rand = new Random();
				int chosenWordIndex = rand.Next(words.Length);
				return words[chosenWordIndex];
			}
			else
			{
				throw new Exception("Le fichier " + WordsFileName + " est introuvable");
			}
		}
	}
}
