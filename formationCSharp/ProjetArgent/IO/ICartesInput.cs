using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO
{
    internal interface ICartesInput
    {
        string ReadCarte();
        void SetupSource(string source);
    }
}
