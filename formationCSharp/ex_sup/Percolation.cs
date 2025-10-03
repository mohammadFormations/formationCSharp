using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public class Percolation
    {
        private readonly bool[,] _open;
        private readonly bool[,] _full;
        private readonly int _size;
        private bool _percolate;

        public Percolation(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Taille de la grille négative ou nulle.");
            }

            _open = new bool[size, size];
            _full = new bool[size, size];
            _size = size;
        }

        public bool IsOpen(int i, int j)
        {
            return _open[i, j];
        }

        private bool IsFull(int i, int j)
        {
            return _full[i, j];
        }

        public bool Percolate()
        {
            for (int i = 0; i < _size; i++)
            {
                if (_full[_size - 1, i]) return true;
            }
            return false;
        }

        private List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> neighbors = new List<KeyValuePair<int, int>>();

            KeyValuePair<int, int> possibleNorthNeighbor = new KeyValuePair<int, int>(i, j - 1);
            KeyValuePair<int, int> possibleSouthNeighbor = new KeyValuePair<int, int>(i, j + 1);
            KeyValuePair<int, int> possibleEastNeighbor = new KeyValuePair<int, int>(i - 1, j);
            KeyValuePair<int, int> possibleWestNeighbor = new KeyValuePair<int, int>(i + 1, j);

            if (IsValidCoordinate(possibleNorthNeighbor)) neighbors.Add(possibleNorthNeighbor);
            if (IsValidCoordinate(possibleSouthNeighbor)) neighbors.Add(possibleSouthNeighbor);
            if (IsValidCoordinate(possibleEastNeighbor)) neighbors.Add(possibleEastNeighbor);
            if (IsValidCoordinate(possibleWestNeighbor)) neighbors.Add(possibleWestNeighbor);

            return neighbors;
        }

        /// <summary>
        /// en ouvrant si l'un des voisins à de l eau il faut faire une recursion sur tout les voisins
        /// qui sont ouvert et les remplir
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Open(int i, int j)
        {
            _open[i,j] = true;
            if (IsFull(i, j)) return;
            if (IsCaseNeighborPercoled(i, j) || i == 0)
            {

                FloodRecursively(i, j);
            }
        }

        public void FloodRecursively(int i, int j)
        {
            _full[i, j] = true;
            foreach (KeyValuePair<int, int> neighbor in CloseNonFloodedOpenNeighbors(i, j))
            {
                FloodRecursively(neighbor.Key, neighbor.Value);
            }
        }

        public bool IsCaseNeighborPercoled(int i ,int j)
        {
            foreach (KeyValuePair<int, int> neighbor in CloseNeighbors(i, j))
            {
                if(_full[neighbor.Key, neighbor.Value]) return true;

            }
            return false;
        }

        public bool IsValidCoordinate(KeyValuePair<int, int> coor)
        {
            return coor.Key >= 0 && coor.Value >= 0 && coor.Key < _size && coor.Value < _size;
        }

        public int countOpenCases()
        {
            int openCases = 0;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    openCases += (_open[i, j] ? 1 : 0);
                }
            }
            return openCases;
        }


        private List<KeyValuePair<int, int>> CloseNonFloodedOpenNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> neighbors = new List<KeyValuePair<int, int>>();

            KeyValuePair<int, int> possibleNorthNeighbor = new KeyValuePair<int, int>(i, j - 1);
            KeyValuePair<int, int> possibleSouthNeighbor = new KeyValuePair<int, int>(i, j + 1);
            KeyValuePair<int, int> possibleEastNeighbor = new KeyValuePair<int, int>(i - 1, j);
            KeyValuePair<int, int> possibleWestNeighbor = new KeyValuePair<int, int>(i + 1, j);

            if (IsValidOpenNonFlooded(possibleNorthNeighbor)) neighbors.Add(possibleNorthNeighbor);
            if (IsValidOpenNonFlooded(possibleSouthNeighbor)) neighbors.Add(possibleSouthNeighbor);
            if (IsValidOpenNonFlooded(possibleEastNeighbor)) neighbors.Add(possibleEastNeighbor);
            if (IsValidOpenNonFlooded(possibleWestNeighbor)) neighbors.Add(possibleWestNeighbor);

            return neighbors;
        }

        private bool IsValidOpenNonFlooded(KeyValuePair<int, int> coor)
        {
            return IsValidCoordinate(coor) && IsOpen(coor.Key, coor.Value) && !IsFull(coor.Key, coor.Value);
        }


    }
}
