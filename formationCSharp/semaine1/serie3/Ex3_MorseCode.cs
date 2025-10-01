using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Serie3
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;
        private readonly Dictionary<char, string> _reversedAlphabet = new Dictionary<char, string>();

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
            };

            // get all alphabet values and then use them as keys 
            Dictionary<string, char>.KeyCollection keyColl = _alphabet.Keys;

            foreach (string key in keyColl)
            {

                bool etat = _alphabet.TryGetValue(key, out char translated);
                if (etat)
                {

                    _reversedAlphabet[translated] = key;
                }
            }

        }

        public int LettersCount(string code)
        {
            // separer les mots
            string cleanString = code.Replace(".......", "?");
            string[] words = cleanString.Split('?');
            int lettersCount = 0;
            for (int i = 0; i < words.Length; i++)
            {
                string cleanWord = words[i].Replace("...", "?");
                string[] letters = cleanWord.Split('?');
                lettersCount += letters.Length;
            }
            return lettersCount;
        }

        public int WordsCount(string code)
        {
            // separer les mots
            string cleanString = code.Replace(".....", "?");
            string[] words = cleanString.Split('?');
            return words.Length;
        }

        public string MorseTranslation(string code)
        {
            string cleanString = code.Replace(".......", "?");
            string[] words = cleanString.Split('?');

            StringBuilder decodedStringBuilder = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string cleanWord = words[i].Replace("...", "?");
                string[] letters = cleanWord.Split('?');
                for (int j = 0; j < letters.Length; j++)
                {
                    decodedStringBuilder.Append(TranslateOneLetter(letters[j]));
                }
            }
            return decodedStringBuilder.ToString();
        }


        public string EfficientMorseTranslation(string code)
        {

            string partialyErrorCorrectedMorse = RegexCorrectMorseWordseperatorError(code);
            return MorseTranslation(partialyErrorCorrectedMorse);


        }
        public string MorseEncryption(string sentence)
        {
            StringBuilder morseEncryptedStringBuilder = new StringBuilder();
            char[] chars = sentence.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ')
                {
                    morseEncryptedStringBuilder.Append(PointWord);
                }
                else
                {
                    bool result = _reversedAlphabet.TryGetValue(chars[i], out string value);
                    if (result)
                    {
                        morseEncryptedStringBuilder.Append(value);
                        morseEncryptedStringBuilder.Append(PointLetter);
                    }
                }

            }
            return morseEncryptedStringBuilder.ToString();
        }


        public string RegexCorrectMorseWordseperatorError(string code)
        {
            string wordSepPattern = @".{ 5 ,}";
            string letterSepPattern = @".{3, 4}";
            string beepSepPattern = @".{1, 2}";
            string wordSepCorrected = Regex.Replace(code, letterSepPattern, PointWord);
            string letterSepCorrected = Regex.Replace(code, wordSepPattern, wordSepCorrected);
            string beepSepCorrected = Regex.Replace(code, beepSepPattern, letterSepCorrected);
            return beepSepCorrected;
        }

        public string EfficientMorseTranslation2(string code)
        {

            string partialyErrorCorrectedMorse = CorrectMorseWordseperatorError(code);

            // replace word separators by ? and letters separators by ! and replace the .. and . 
            // used for ti et taah separation by .
            StringBuilder stringBuilder = new StringBuilder();
            string readyMorse = stringBuilder.Append(partialyErrorCorrectedMorse)
                .Replace(PointWord, "?")
                .Replace(PointLetter, "!")
                .Replace("..", Point)
                .ToString();

            Console.WriteLine("lalalaalal        " + readyMorse);

            string[] cleanWords = readyMorse.Split('?');

            StringBuilder decodedStringBuilder = new StringBuilder();

            for (int i = 0; i < cleanWords.Length; i++)
            {
                string[] letters = cleanWords[i].Split('!');

                for (int j = 0; j < letters.Length; j++)
                {
                    decodedStringBuilder.Append(TranslateOneLetter(letters[j]));
                }
            }
            return decodedStringBuilder.ToString();

        }



        public char TranslateOneLetter(string morseLetter)
        {
            bool etat = _alphabet.TryGetValue(morseLetter, out char translated);
            return etat ? translated : '+';

        }

        public string CorrectMorseWordseperatorError(string code)
        {
            // eliminer tout ce qui est plus que 5 points
            StringBuilder correctedMorseStringBuilder = new StringBuilder();

            /*
             iterer sur toutes les sous chaines de 10 characteres si elles sont des dates non socialistes on les socialize
             */
            for (int i = 0; i < code.Length - PointWord.Length; i++)
            {
                string PossibleWordStop = code.Substring(i, PointWord.Length);
                if (String.Compare(PossibleWordStop, PointWord) == 0)
                {
                    correctedMorseStringBuilder.Append(PointWord);
                    i += PointWord.Length;
                    for (int j = i + 1; j < code.Length - PointWord.Length; j++)
                    {
                        if (String.Compare(code.Substring(j), Point) == 0)
                        {
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    correctedMorseStringBuilder.Append(code.Substring(i));
                }

            }

            return correctedMorseStringBuilder.ToString();

        }

    }
}
