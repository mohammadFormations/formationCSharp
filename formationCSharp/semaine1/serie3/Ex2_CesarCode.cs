using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    public class Cesar
    {
        private readonly char[,] _cesarTable;

        public Cesar()
        {
            _cesarTable = new char[,]
            {
                { 'A', 'D' },
                { 'B', 'E' },
                { 'C', 'F' },
                { 'D', 'G' },
                { 'E', 'H' },
                { 'F', 'I' },
                { 'G', 'J' },
                { 'H', 'K' },
                { 'I', 'L' },
                { 'J', 'M' },
                { 'K', 'N' },
                { 'L', 'O' },
                { 'M', 'P' },
                { 'N', 'Q' },
                { 'O', 'R' },
                { 'P', 'S' },
                { 'Q', 'T' },
                { 'R', 'U' },
                { 'S', 'V' },
                { 'T', 'W' },
                { 'U', 'X' },
                { 'V', 'Y' },
                { 'W', 'Z' },
                { 'X', 'A' },
                { 'Y', 'B' },
                { 'Z', 'C' }
            };
        }

        public string CesarCode(string line)
        {
            // uniquelent majiscule pour l instant et sans espace
            StringBuilder encryptedLineBuilder = new StringBuilder();
            char[] chars = line.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                encryptedLineBuilder.Append(CesarUnChar(chars[i]));
            }

            return encryptedLineBuilder.ToString(); 
        }

        public string DecryptCesarCode(string line)
        {
            // uniquelent majiscule pour l instant et sans espace
            StringBuilder decryptedLineBuilder = new StringBuilder();
            char[] chars = line.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                decryptedLineBuilder.Append(CesarUnChar(chars[i], 1, 0));
            }

            return decryptedLineBuilder.ToString();
        }

        public string GeneralCesarCode(string line, int x)
        {
            // les characters couvert par cet algorithm: du A jusqu'a Z
            StringBuilder shiftedLineBuilder = new StringBuilder();
            char[] chars = line.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                shiftedLineBuilder.Append(GeneralCesarUnChar(chars[i], x));
            }


            return shiftedLineBuilder.ToString();
        }

        public string GeneralDecryptCesarCode(string line, int x)
        {
            //TODO
            return GeneralCesarCode(line, 26 - x);
        }



        public char GeneralCesarUnChar(char c, int stepSize)
        {
            // A ascii 65
            // Z ascii 90
            return (char)(((int)c - 65 + stepSize) % 26 + 65);
        }


        /// <summary>
        /// fonction qui fait un mapping entre les characteres avant dechiffrage et apres dechiffrage
        /// </summary>
        /// <param name="c"></param>
        /// <param name="indice1"> l'indice de la liste des characters en entré</param>
        /// <param name="indice2"> l'indice de la liste des characters en sortie</param>
        /// <returns></returns>
        public char CesarUnChar(char c, int indice1=0, int indice2=1)
        {
            for (int i = 0; i < _cesarTable.GetLength(0); i++)
            {
                if (_cesarTable[i, indice1] == c)
                {
                    return _cesarTable[i, indice2];
                }
            }
            return '\0';
        }

    }
}
