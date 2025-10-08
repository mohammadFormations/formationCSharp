using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO
{
    internal interface ITransactionInput
    {

        string ReadTransaction();
        void SetupSource(string source);
    }
}
