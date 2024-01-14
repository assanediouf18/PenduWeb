using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pendu.Services.WordProviders;

namespace Pendu.Pages
{
    public class JeuModel : PageModel
    {
        public JeuModel(IWordProvider wordProvider) {
            WordProvider = wordProvider;
        }

        public IWordProvider WordProvider { get; }
        public String? NextWord { get; private set; }

        public void OnGet()
        {
            NextWord = WordProvider.GetWord();
        }
    }
}
