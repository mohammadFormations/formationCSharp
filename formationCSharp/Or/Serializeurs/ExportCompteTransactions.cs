using System.Collections.Generic;
using System.Xml.Serialization;
using Or.Models;

namespace Or.Serializeurs
{
    public class ExportCompteTransactions
    {


        public ExportCompteTransactions() { }

        [XmlElement("Transaction")]
        public List<Transaction> Transactions { get; set; }
    }
}
