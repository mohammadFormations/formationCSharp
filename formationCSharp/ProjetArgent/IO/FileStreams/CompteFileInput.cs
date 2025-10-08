using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.FileStreams
{
    public class CompteFileInput
    {
        private FileStream fs;
        private StreamReader reader;

        public CompteFileInput(string path)
        {
            fs = File.OpenRead(path);
            reader = new StreamReader(fs);
        }
        public string ReadCompte()
        {
            return reader.ReadLine();
        }

        public void close()
        {
            reader.Close();
        }
    }
}
