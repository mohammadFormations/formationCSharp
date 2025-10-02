using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Morpion
    {
        /// <summary>
        /// afficher la grille morpion
        /// </summary>
        /// <param name="grille"></param>
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

        /// <summary>
        /// ce methode permet d'avoir un caractère representant du mouvement effectuée
        /// par un  utilisateur
        /// 1 pour X
        /// -1 pour O
        /// 0 pour _ (case vide)
        /// </summary>
        /// <param name="signe"></param>
        /// <returns></returns>
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

        /// <summary>
        /// retourne un code signalant l'etat de la grille morpion
        ///
        /// </summary>
        /// <param name="grille"></param>
        /// <returns>
        /// return 1 : joueur 1 gagnant
        /// return 2 : joueur 2 gagnant
        /// return 0 : egalite joueur 1 et joueur 2
        /// return -1: jeux non terminé
        /// 
        /// </returns>
        public static int CheckMorpion(int[,] grille)
        {
            int[][,] masques_gagnants = GetWinningMasks();
            for (int i = 0; i < masques_gagnants.GetLength(0); i++)
            {
                int resultat = ScalMatrcies(grille, masques_gagnants[i]);
                if (resultat == 3) return 1;
                if (resultat == -3) return 2;

            }
            if (!IsDoneMatrice(grille)) return -1;
            return 0;
        }


        /// <summary>
        /// faire l'equivalent de produit scalaire pour des array de 2 dimentions
        /// matrice et mask
        /// quelque soit la valeur de i et j on a:
        ///     somme (matrice[i,j] * mask[i,j]) 
        ///     
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
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

        /// <summary>
        /// verifier si la matrice est tout rempli ou pas
        /// une matrice rempli c'est une matrices qui ne contient aucune
        /// case vide
        /// </summary>
        /// <param name="matrice"></param>
        /// <returns></returns>
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

        /// <summary>
        /// retourn tout les masques gagnants d'un jeux morpion
        /// example d'un masque gagnant
        /// 1 1 1
        /// 0 0 0
        /// 0 0 0
        /// cela indique que une grille
        /// x x x
        /// ? ? ?
        /// ? ? ?
        /// est gagnantes pour le joueur avec le symbol x quelque soit les 
        /// autres cases de la grille en se rend compte que on a maximum un joueur gagnant
        /// par grille
        /// </summary>
        /// <returns></returns>
        public static int[][,] GetWinningMasks()
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
            return masques_gagnants;
        }
    }
}