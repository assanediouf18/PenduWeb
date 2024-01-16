using Pendu.Services.WordProviders;
using System.Text;

namespace Pendu.Models
{
    public class JeuPendu
    {
        private IWordProvider WordProvider;
        private int _mistakes = 0;
        private string _unveiled = "";
        private string _secret = "";

        public readonly int NbAllowedMistakes = 10;

        public JeuPendu(IWordProvider provider)
        {
            WordProvider = provider;
        }

        public int Mistakes
        {
            get
            {
                return _mistakes;
            }
            set
            {
                _mistakes = value;
            }
        }

        public string KnownLetters
        {
            get
            {
                return _unveiled;
            }
            set { _unveiled = value; }
        }

        public string SecretWord
        {
            get
            {
                return _secret;
            }
        }

        public bool Won
        {
            get
            {
                return _unveiled == SecretWord;
            }
        }

        public bool Lost
        {
            get
            {
                return _mistakes >= NbAllowedMistakes;
            }
        }

        public void SetMistakes(int m)
        {
            _mistakes = m;
        }

        public void Start()
        {
            _secret = WordProvider.GetWord().ToUpper();

            for (int i = 0; i < _secret.Length; i++)
            {
                if (i == 0)
                {
                    _unveiled += _secret[0];
                }
                else
                {
                    _unveiled += "_";
                }
            }
        }

        public bool IsGoodGuess(string guess)
        {
            guess = guess.ToUpper();
            bool found = false;
            if (guess.Length == 1)
            {
                StringBuilder newUnveiled = new StringBuilder(_unveiled);
                for (int i = 0; i < SecretWord.Length; i++)
                {
                    if (_unveiled.Contains(guess)) return true;
                    if (SecretWord[i] == guess[0])
                    {
                        newUnveiled[i] = guess[0];
                        found = true;
                    }
                }
                _unveiled = newUnveiled.ToString();
            }
            else
            {
                found = SecretWord == guess;
                if (found)
                {
                    _unveiled = guess;
                }
            }

            if (!found) _mistakes++;
            return found;
        }

        public bool IsGoingOn()
        {
            return !Won && !Lost;
        }
    }
}
