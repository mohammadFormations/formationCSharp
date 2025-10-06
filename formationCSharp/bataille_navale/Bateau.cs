using System.Collections.Generic;
using System.Linq;
using static Bataille_Navale.Position;

namespace Bataille_Navale
{
    internal class Bateau
    {
        public string Nom { get; private set; }
        public int Taille { get; private set; }
        public List<Position> Positions { get; private set; }

        public Bateau(string nom, int taille, List<Position> position)
        {
            Nom = nom;
            Taille = taille;
            Positions = position;
        }

        /// <summary>
        /// Case à l'état touché si elle appartient au bateau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Touché(int x, int y)
        {
            foreach (Position position in Positions)
            {
                if (position.X == x && position.Y == y)
                {
                    position.Touché();
                }
            }
        }

        /// <summary>
        /// Le bateau est-il coulé ? 
        /// </summary>
        public bool EstCoulé()
        {
            foreach (Position position in Positions)
            {
                if (position.Statut != Etat.Touché || position.Statut != Etat.Coulé)
                {
                    return false;
                }
            }
            return true;
        }

    }
}