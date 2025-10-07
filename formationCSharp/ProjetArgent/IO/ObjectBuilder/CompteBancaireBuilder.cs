using ProjetArgent.GestionBancaire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.serializeurs
{
    public class CompteBancaireDeserializer
    {
        private const decimal _defaultSolde = 0;
        private decimal _solde = 0;
        private int _id = 0;
        private TypeCompteEnum.TypeCompte _type;
        private CarteBancaire _carteBancaire;

        public bool ExtractCompteFromLine(string line, List<CarteBancaire> cartes, List<CompteBancaire> comptes, out CompteBancaire compteBancaire)
        {
            compteBancaire = null;
            if (line == null) return false;
            string[] elements = line.Split(';').ToList().FindAll(el => string.Compare(el, "") != 0).ToArray<string>();
            if (elements.Length < 3 || elements.Length > 4) return false;


            if (!SetId(elements[0], comptes)) return false;
            if (!SetCarteBancaire(elements[1], cartes)) return false;
            if (!SetType(elements[2])) return false;

            if (elements.Length == 4 && !SetSolde(elements[3])) return false;
            if (elements.Length == 3) _solde = _defaultSolde;


            compteBancaire = new CompteBancaire(_id, _type, _carteBancaire, _solde);
            return true;
        }

        private bool SetCarteBancaire(string carteBancaireId, List<CarteBancaire> cartes)
        {
            if (carteBancaireId == null || carteBancaireId.Length != 16) return false;

            bool state = long.TryParse(carteBancaireId, out long num);
            if (!state) return false;

            if (num < 0) return false;

            foreach (CarteBancaire carte in cartes)
            {
                if (carte.Numero == carteBancaireId)
                {
                    _carteBancaire = carte;
                    return true;
                }
            }

            return false;
        }

        private bool SetId(string id, List<CompteBancaire> comptes)
        {
            bool state = int.TryParse(id, out _id);
            if (!state) return false;

            foreach (CompteBancaire compte in comptes)
            {
                if (compte.Id == _id)
                {
                    return false;
                }
            }

            return true;
        }

        private bool SetType(string type)
        {
            bool state = Enum.TryParse(type, out TypeCompteEnum.TypeCompte parsedType);
            if (!state) return false;
            _type = parsedType;
            return true;
        }

        private bool SetSolde(string solde)
        {
            bool state = decimal.TryParse(solde, NumberStyles.Number, CultureInfo.InvariantCulture, out _solde);
            if (!state || _solde < 0) return false;
            return true;
        }

    }
}
