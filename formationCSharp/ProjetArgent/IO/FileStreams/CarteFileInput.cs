using System.IO;

namespace ProjetArgent.IO.FileStreams
{
    public class CarteFileInput
    {
        private FileStream fs;
        private StreamReader reader;

        public CarteFileInput(string path)
        {
            fs = File.OpenRead(path);
            reader = new StreamReader(fs);
        }

        public string ReadCarte()
        {
            return reader.ReadLine();
        }

        public void close()
        {
            reader.Close();
        }
    }
}
