using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetArgent.IO.FileStreams
{
    public class CarteFileInput
    {
        private FileStream fs;
        private StreamReader reader;

        CarteFileInput(string path)
        {
            fs = File.OpenRead(path);
            reader = new StreamReader(fs);
        }

        public string ReadCarte()
        {
            return reader.ReadLine();
        }
    }
}
