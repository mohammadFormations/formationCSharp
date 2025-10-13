using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO
{
    // tu n'utilises pas ces interfaces 
    internal interface ICartesInput
    {
        string ReadCarte();
        void SetupSource(string source);
    }
}
