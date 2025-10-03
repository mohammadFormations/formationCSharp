using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Bataille_Navale.Position;

namespace Bataille_Navale
{
    internal class Plateau
    {
        public Position[,] PlateauJeu { get; set; }

        public List<Bateau> Bateaux { get; set; }

        public Plateau()
        {
            PlateauJeu = new Position[10, 10];
            Bateaux = new List<Bateau>()
            {
               new Bateau("A", 5, new List<Position>()),
               new Bateau("B", 4, new List<Position>()),
               new Bateau("C", 3, new List<Position>()),
               new Bateau("D", 3, new List<Position>()),
               new Bateau("E", 2, new List<Position>())
            };
        }

        public void CreationPlateau()
        {
            for (int i=0; i< 10; i++)
            {
                for (int j=0; j < 10; j++)
                {
                    PlateauJeu[i, j] = new Position(i, j);
                }
            }

            Random random = new Random();
            foreach(Bateau bateau in Bateaux)
            {
                int taille = bateau.Taille;
                bool isVertical = random.Next(2) == 0;
                int limX = 10 - (isVertical ? taille : 0);
                int limY = 10 - (isVertical ? 0 : taille);

                while (true)
                {
                    int proposedX = random.Next(limX);
                    int proposedY = random.Next(limY);
                    if (PlacerBateau(proposedX, proposedY, taille, isVertical)) break;
                }

            }
        }
        
        public void LancementPartie()
        {
            CreationPlateau();
            int cpt = 0;
            while (!FindePartie())
            {
                Console.Clear();
                AfficherPlateau();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Quelle case visez-vous : (format: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ligne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(",");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("colonne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(")");
                Console.WriteLine();

                string val = Console.ReadLine();
                string[] position = val.Split(',', '.');
                // Partie à implémenter

                bool validPosition = ValidatePosition(position, out int x, out int y);
                if (validPosition)
                {
                    cpt++;
                    Viser(x-1, y-1);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            AfficherPlateau();
            Console.Write($"GG {cpt} coups effectués !");
        }

        public bool ValidatePosition(string[] position, out int x, out int y)
        {
            x = 0;
            y = 0;
            if (position.GetLength(0) != 2) return false;
            bool validCoor1 = Int32.TryParse(position[0], out x) && x<=10 && x>0;
            if (!validCoor1) return false;

            bool validCoor2 = Int32.TryParse(position[1], out y) && y <= 10 && y > 0;
            if (!validCoor2) return false;

            return true;
        }

        /// <summary>
        /// Peut-on placer le navire sur la grille sans qu'il dépasse les bords et qu'il ne touche les autres bateaux ? 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="taille"></param>
        /// <param name="estVertical"></param>
        /// <returns></returns>
        private bool PlacerBateau(int x, int y, int taille, bool estVertical)
        {

            Console.WriteLine(x + " " + y + " " + taille + " " + estVertical);
            // validation
            if (!estVertical)
            {
                for (int i = x; i < x + taille; i++)
                {
                    /*if (PlateauJeu[i, y].Statut != Etat.Caché) return false;*/
                    foreach(Bateau bateau in Bateaux)
                    {
                        foreach(Position pos in bateau.Positions)
                        {
                            for (int k = Math.Max(pos.X-1, 0); k < Math.Min(pos.X + 2, 10); k++)
                            {
                                for (int l = Math.Max(pos.Y - 1, 0); l < Math.Min(pos.Y + 2, 10); l++)
                                {
                                    if(i == k && y == l)
                                    {
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                }
            } else
            {
                for (int j = y; j < y + taille; j++)
                {
                    /*if (PlateauJeu[i, y].Statut != Etat.Caché) return false;*/
                    foreach (Bateau bateau in Bateaux)
                    {
                        foreach (Position pos in bateau.Positions)
                        {
                            for (int k = Math.Max(pos.X - 1, 0); k < Math.Min(pos.X + 2, 10); k++)
                            {
                                for (int l = Math.Max(pos.Y - 1, 0); l < Math.Min(pos.Y + 2, 10); l++)
                                {
                                    if (x == k && j == l)
                                    {
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            // placement
            foreach (Bateau bateau in Bateaux)
            {
                if (bateau.Taille == taille && bateau.Positions.Count == 0)
                {
                    if (estVertical)
                    {
                        for (int i = x; i < x + taille; i++)
                        {
                            bateau.Positions.Add(new Position(i, y));
                        }
                    }
                    else
                    {
                        for (int j = y; j < y + taille; j++)
                        {
                            bateau.Positions.Add(new Position(x, j));
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Choix de la case (x , y) 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Viser(int x, int y)
        {
            foreach (Bateau bateau in Bateaux)
            {
                foreach(Position position in bateau.Positions)
                {
                    if(position.X == x && position.Y == y)
                    {
                        PlateauJeu[x, y].Touché();
                        CouleIfPossible(bateau);
                        return;
                    }
                }
            }
            PlateauJeu[x, y].Plouf();
        }

        /// <summary>
        /// Affichage de l'état de la grille et de la situation de la partie
        /// </summary>
        public void AfficherPlateau()
        {
            List<Position> list = new List<Position>();
            foreach (Bateau b in Bateaux)
            {
                list.AddRange(b.Positions);
                Console.WriteLine($"{b.Nom}: {b.Taille} de long, coulé: {b.EstCoulé()}");
            }

            foreach (Position p in list)
            {
                PlateauJeu.SetValue(p, p.X, p.Y);
                Console.Write(p.X + " " + p.Y + "     ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
            int cpt = 0, tmp = 0;
            foreach (Position p in PlateauJeu)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                if (p.X != tmp || cpt == 0)
                {
                    if (cpt > 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(string.Format("{0,-3}", ++cpt));
                }

                ConsoleColor foreground;
                switch (p.Statut)
                {
                    case Position.Etat.Plouf:
                        foreground = ConsoleColor.Blue;
                        break;
                    case Position.Etat.Touché:
                        foreground = ConsoleColor.Red;
                        break;
                    case Position.Etat.Coulé:
                        foreground = ConsoleColor.Green;
                        break;
                    default:
                        foreground = ConsoleColor.White;
                        break;
                }
                Console.ForegroundColor = foreground;
                Console.Write((char)p.Statut + " ");

                tmp = p.X;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// La partie est-elle finie ? 
        /// </summary>
        /// <returns></returns>
        internal bool FindePartie() 
		{
            foreach(Bateau bateau in Bateaux)
            {
                if (bateau.Positions.Count() == 0 || bateau.Positions.First().Statut != Etat.Coulé) return false; 
            }
			return true;
		}

        public static void CouleIfPossible(Bateau bateau)
        {
            foreach(Position pos in bateau.Positions)
            {
                if (pos.Statut != Etat.Touché)
                {
                    return;
                }
            }

            foreach (Position pos in bateau.Positions)
            {
                pos.Coulé();
            }

        }
    }
}