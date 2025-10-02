using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie4
{
    public static class Morpion
    {
        public static void MorpionGame()
        {
            int[,] grille = GetstartingMorpionGrid();
            Console.WriteLine("Debut de partie de Morpion");

            int gameState = CheckMorpion(grille);
            bool isPlayer1Turn = true;
            while (gameState == -1)
            {
                Play1MorpionRound(grille, isPlayer1Turn);
                isPlayer1Turn = !isPlayer1Turn;
                gameState = CheckMorpion(grille);
            }
            switch (gameState)
            {
                case 0:
                    Console.WriteLine("wow bien jouée vous êtes fort");
                    break;
                case 1:
                    Console.WriteLine("Joueur 1 tu es gagnant");
                    break;
                case 2:
                    Console.WriteLine("jouer 2 jeux fantastique");
                    break;
                default:
                    Console.WriteLine("c'est con");
                    break;
            }

        }

        public static void Play1MorpionRound(int[,] grille, bool isPlayer1Turn)
        {
            ForceValidMove(isPlayer1Turn, grille, out int coor1, out int coor2);
            ApplyUserMove(isPlayer1Turn, grille, coor1, coor2);
            DisplayMorpion(grille);

        }

        public static void ApplyUserMove(bool isPlayer1Turn, int[,] grille, int coor1, int coor2)
        {
            int playerNumber = (isPlayer1Turn? 1 : -1);
            grille[coor1, coor2] = playerNumber;
        }


        public static void ForceValidMove(bool isPlayer1Turn, int[,] grille, out int coor1, out int coor2)
        {
            coor1 = 0;
            coor2 = 0;
            bool isValidMove = false;
            while (!isValidMove)
            {
                Console.Write("Coup du Joueur " + (isPlayer1Turn ? "1" : "2") + " :");
                string userMove = Console.ReadLine();
                isValidMove = ValidateUserMove(userMove, out coor1, out coor2);
                isValidMove = isValidMove && ValidateGridPosition(grille, coor1, coor2);
                if (!isValidMove) Console.WriteLine("coup incorrect, veuillez reessayez.");
            }
        }

        public static bool ValidateGridPosition(int [,] grille, int coor1, int coor2)
        {
            return grille[coor1, coor2] == 0;
        }


        public static bool ValidateUserMove(string userMove, out int coor1, out int coor2)
        {
            coor1 = 0;
            coor2 = 0;
            char[] userMoveCoordinates = userMove.ToCharArray();
            return ValidateUserMoveChar1(userMoveCoordinates[0], out coor1)
                && ValidateUserMoveChar2(userMoveCoordinates[1], out coor2);

        }

        public static bool ValidateUserMoveChar2(char char2, out int coor2)
        {
            bool state = Int32.TryParse(char2.ToString(), out int coor2Start1);
            coor2 = coor2Start1 -1;
            if (!state) return false;
            if (coor2 < 0 || coor2 > 2) return false;

            return true;
        }

        public static bool ValidateUserMoveChar1(char char1, out int coor1)
        {
            coor1 = (int)char1 - (int)'A';
            if (char1 < 'A' || char1 > 'C') return false;
            if (char1 < 'A' || char1 > 'C') return false;
            return true;
        }

        public static int[,] GetstartingMorpionGrid()
        {
            int[,] morpion = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            return morpion;

        }

        public static void DisplayMorpion(int[,] grille)
        {
            Console.WriteLine("Affichage grid Morpion :");
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Console.Write(GetChar(grille[i, j]) + " ");
                }
                Console.WriteLine();
            }
            return;
        }

        public static char GetChar(int signe)
        {
            switch (signe)
            {
                case 1:
                    return 'X';
                case -1:
                    return 'O';
                default:
                    return '_';
            }
        }

        public static int CheckMorpion(int[,] grille)
        {
            int[][,] masques_gagnants = {
                new int[,] {
                    {1, 1, 1}, {0, 0, 0}, {0, 0, 0}
                },
                new int[,] {
                    {0, 0, 0}, {1, 1, 1}, {0, 0, 0}
                },
                new int[,] {
                    {0, 0, 0}, {0, 0, 0}, {1, 1, 1}
                },
                new int[,] {
                    {1, 0, 0}, {1, 0, 0}, {1, 0, 0}
                },
                new int[,] {
                    {0, 1, 0}, {0, 1, 0}, {0, 1, 0}
                },
                new int[,] {
                    {0, 0, 1}, {0, 0, 1}, {0, 0, 1}
                },
                new int[,] {
                    {1, 0, 0}, {0, 1, 0}, {0, 0, 1}
                },
                new int[,] {
                    {0, 0, 1}, {0, 1, 0}, {1, 0, 0}
                }
            };
            for (int i = 0; i < masques_gagnants.GetLength(0); i++)
            {
                int resultat = ScalMatrcies(grille, masques_gagnants[i]);
                if (resultat == 3) return 1;
                if (resultat == -3) return 2;

            }
            if (!IsDoneMatrice(grille)) return -1;
            return 0;
        }


        public static int ScalMatrcies(int[,] matrice, int[,] mask)
        {
            int scal = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    scal += matrice[i, j] * mask[i, j];
                }

            }
            return scal;
        }
        public static bool IsDoneMatrice(int[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    if (matrice[i, j] == 0) return false;
                }

            }
            return true;
        }
    }
}
