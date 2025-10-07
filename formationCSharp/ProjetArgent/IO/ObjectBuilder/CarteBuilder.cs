using ProjetArgent.GestionBancaire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.ObjectBuilder
{
    // les deserializers des classes static
    public class CarteDeserializer
    {
        private string _numero;
        private decimal _plafond;
        private const decimal _defaultPlafond = 500;

        public bool ExtractCarteFromLine(string line, out  CarteBancaire carteBancaire)
        {
            carteBancaire = null;
            if (line == null) return false;
            string[] elements = line.Split(';').ToList().FindAll(el => string.Compare(el, "") != 0).ToArray<string>();
            if (elements.Length == 0 || elements.Length > 2) return false;


            _numero = elements[0];
            if (!VerifyNumero()) return false;

            if (elements.Length == 2)
            {
                bool state = decimal.TryParse(elements[1], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal plaf);
                if (!state) return false;
                if (plaf < 0) return false;
                _plafond = (decimal)((int)plaf / 100) * 100;
            }
            else
            {
                _plafond = _defaultPlafond;
                if (!VerifyPlafond()) return false;
            }
            carteBancaire = new CarteBancaire(_numero, _plafond);
            return true;
        }


        private bool VerifyNumero()
        {
            if (_numero == null || _numero.Length != 16) return false;

            bool state = long.TryParse(_numero, out long num);
            if (!state) return false;

            if (num < 0) return false;
            return true;
        }

        private bool VerifyPlafond()
        {
            if(_plafond > 3000 || _plafond < 500) return false;
            return true;
        }

    }
}
