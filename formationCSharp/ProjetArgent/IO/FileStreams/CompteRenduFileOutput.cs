using ProjetArgent.GestionBancaire.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.FileStreams
{
    public class CompteRenduFileOutput
    {
        private FileStream fs;
        private StreamWriter writer;

        public CompteRenduFileOutput(string path)
        {
            fs = File.OpenWrite(path);
            writer = new StreamWriter(fs);
        }
        public void writeCompteRendu(string compteRendu)
        {
            writer.WriteLine(compteRendu);
        }

        public void FormatEtEcritCompteRendu(int transactionId, bool status)
        {
            writeCompteRendu($"{transactionId};{(status ? StatusTransactionEnum.StatusTransaction.OK : StatusTransactionEnum.StatusTransaction.KO)}");
        }

        public void close()
        {
            writer.Close();
        }

    }
}
