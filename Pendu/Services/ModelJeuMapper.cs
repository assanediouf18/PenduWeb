using Pendu.Models;
using Pendu.Services.WordProviders;

namespace Pendu.Services
{
	public class ModelJeuMapper
	{
		public JeuPendu GetJeuPendu(PenduModel model)
		{
			JeuPendu jeuPendu = new JeuPendu(new OneWordProvider(model.Secret));
			jeuPendu.Start();
			jeuPendu.SetMistakes(model.NbMistakes);
			if(model.KnownLetters != "")
			{
				jeuPendu.KnownLetters = model.KnownLetters;
			}
			return jeuPendu;
		}
	}
}
