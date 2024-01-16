using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pendu.Services.WordProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Pendu.Services;
using Pendu.Data;
using Pendu.Models;
using Microsoft.EntityFrameworkCore;

namespace Pendu.Pages
{
    public class JeuModel : PageModel
    {
        public JeuModel(IWordProvider wordProvider, PenduContext penduContext, ModelJeuMapper modelJeuMapper, ILogger<JeuModel> logger) {
            WordProvider = wordProvider;
            _context = penduContext;
            JeuMapper = modelJeuMapper;
			_logger = logger;
		}

        public IWordProvider WordProvider { get; }
        public const String SessionIdPath = "_GameId";
        public JeuPendu Jeu { get; set; }
        private readonly PenduContext _context;
        public ModelJeuMapper JeuMapper { get; set; }
		private readonly ILogger<JeuModel> _logger;

		public async Task<IActionResult> OnGetAsync()
        {
			PenduModel model = await GetCurrentModelAsync();
			if (model == null)
			{
				return NotFound();
			}
			Jeu = JeuMapper.GetJeuPendu(model);
            return Page();
		}

		public IActionResult OnGetNewGame()
		{
			HttpContext.Session.Remove(SessionIdPath);
			return RedirectToPage(nameof(Jeu));
		}

        public async Task<IActionResult> OnGetIsGoodGuessAsync(string guess)
        {
            PenduModel model = await GetCurrentModelAsync();
            if(model == null)
            {
                return NotFound();
            }
			Jeu = JeuMapper.GetJeuPendu(model);
            Jeu.IsGoodGuess(guess);

			model.KnownLetters = Jeu.KnownLetters;
			model.NbMistakes++;

			_context.Attach(model).State = EntityState.Modified;
			await _context.SaveChangesAsync();


            return Page();
		}

        private async Task<PenduModel> GetCurrentModelAsync()
        {
			PenduModel model;
			int? id = HttpContext.Session.GetInt32(SessionIdPath);
			if (id == null)
			{
				model = new PenduModel();
				model.Secret = WordProvider.GetWord();
				model.KnownLetters = "";
				model.NbMistakes = 0;

				_context.PenduModel.Add(model);
				await _context.SaveChangesAsync();

				HttpContext.Session.SetInt32(SessionIdPath, model.Id);
			}
			else
			{
				var queryRes = await _context.PenduModel.FirstOrDefaultAsync(m => m.Id == id);
				if (queryRes != null)
				{
					model = queryRes;
				}
				else
				{
					return null;
				}
			}
            return model;
		}
    }
}
