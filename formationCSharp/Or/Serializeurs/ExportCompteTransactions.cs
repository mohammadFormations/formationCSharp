using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Or.Business;
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

