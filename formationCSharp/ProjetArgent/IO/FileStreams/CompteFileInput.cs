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

        CompteFileInput(string path)
        {
            fs = File.OpenRead(path);
            reader = new StreamReader(fs);
        }
        public string ReadCompteRendu()
        {
            return reader.ReadLine();
        }
    }
}
