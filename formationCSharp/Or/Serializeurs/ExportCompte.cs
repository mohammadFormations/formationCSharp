using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Or.Models;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Numerics;

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
