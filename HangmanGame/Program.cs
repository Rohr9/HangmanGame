namespace HangmanGame
{
    class Program
    {
        static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ".ToLower().ToCharArray();
        static string theWord;
        static char[] guessedWord;
        static char[] guessedLetters = new char[26];
        static int guessedCount = 0;
        static int attempts;

        static void Main(string[] args)
        {
            SetupGame();

            if (IsValidWord(theWord))
            {
                PlayGame();
                Stilling();
            }
            else
            {
                Console.WriteLine("Ikke godkendt - brug kun bogstaver");
            }
        }

        static void SetupGame()
        {
            Console.WriteLine("Hvilket ord skal gættes: ");
            theWord = Console.ReadLine().ToLower();
            Console.Clear();

            guessedWord = new string('_', theWord.Length).ToCharArray();
            attempts = 6;

            Console.WriteLine("Gæt ordet: ");
        }

        static bool IsValidWord(string word)
        {
            foreach (char c in word)
            {
                if (Array.Exists(alphabet, letter => letter == c))
                {
                    return true;
                }
            }
            return false;
        }

        static void PlayGame()
        {
            while (AnyLifeLeft() && !IsWordGuessed())
            {
                DisplayGameState();
                string guess = GuessLetterOrWord();

                if (guess.Length == 1)
                {
                    char guessedLetter = guess[0];

                    if (GuessedLetter(guessedLetter))
                    {
                        Console.Clear();
                        Console.WriteLine("Du har allerede gættet dette bogstav - prøv med et andet");
                        continue;
                    }

                    guessedLetters[guessedCount++] = guessedLetter;

                    if (IsLetterInWord(guessedLetter))
                    {
                        Console.WriteLine("Korrekt!");
                        Console.Clear();
                    }
                    else
                    {
                        PlayerLoseALife();
                        Console.WriteLine("Forkert!");
                        Console.Clear();
                    }
                }
                else
                {
                    if (GuessedWord(guess))
                    {
                        Console.WriteLine("Tillykke du gættede ordet!");
                        break;
                    }
                    else
                    {
                        PlayerLoseALife();
                        Console.WriteLine("Forkert ord!");
                    }
                }
            }
        }

        static void DisplayGameState()
        {
            Console.WriteLine($"\nGæt ordet: {new string(guessedWord)}");
            Console.WriteLine($"Forsøg tilbage: {attempts}");
        }

        static string GuessLetterOrWord()
        {
            Console.Write("Gæt bogstav eller hele ordet: ");
            return Console.ReadLine().ToLower();
        }

        static bool GuessedLetter(char letter)
        {
            for (int i = 0; i < guessedCount; i++)
            {
                if (guessedLetters[i] == letter)
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsLetterInWord(char letter)
        {
            bool isInWord = false;

            for (int i = 0; i < theWord.Length; i++)
            {
                if (theWord[i] == letter)
                {
                    guessedWord[i] = letter;
                    isInWord = true;
                }
            }

            return isInWord;
        }

        static bool GuessedWord(string guess)
        {
            if (guess == theWord)
            {
                guessedWord = theWord.ToCharArray();
                return true;
            }
            return false;
        }

        static bool IsWordGuessed()
        {
            return !new string(guessedWord).Contains('_');
        }

        static void PlayerLoseALife()
        {
            attempts--;
        }

        static bool AnyLifeLeft()
        {
            return attempts > 0;
        }

        static void Stilling()
        {
            if (IsWordGuessed())
            {
                Console.WriteLine($"\nTillykke! Du gættede ordet: {theWord}");
            }
            else
            {
                Console.WriteLine($"\nBedre held næste gang. Ordet var: {theWord}");
            }
        }
    }
}
