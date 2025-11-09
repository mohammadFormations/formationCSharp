using System.Collections.Generic;
using Or.Models;
using System.Xml.Serialization;

namespace Or.Serializeurs
{
    [XmlType("Comptes")]
    public class ExportCompte
    {

        public ExportCompte() { }

        //[XmlElement("Comptes")]

        [XmlElement("Compte", typeof(Compte))]
        public List<Compte> Comptes { get; set; }
    }
}
