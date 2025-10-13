using ProjetArgent.GestionBancaire.Enums;
using System.IO;

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
