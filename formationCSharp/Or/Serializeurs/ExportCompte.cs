using System.Collections.Generic;
using Or.Models;
using System.Xml.Serialization;

namespace Or.Serializeurs
{
    [XmlRoot]
    public class ExportCompte
    {

        public ExportCompte() { }

        //[XmlElement("Comptes")]

        [XmlArray("Comptes")]
        [XmlArrayItem("Compte", typeof(Compte))]
        public List<Compte> Comptes { get; set; }
    }
}
