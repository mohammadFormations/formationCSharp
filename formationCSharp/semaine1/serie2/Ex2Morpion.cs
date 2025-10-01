using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Morpion
    {
        public static void DisplayMorpion(int[,] grille)
        {
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Console.Write(GetChar(grille[i, j])+ " ");
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