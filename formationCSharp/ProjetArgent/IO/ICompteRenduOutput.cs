using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO
{
    internal interface ICompteRenduOutput
    {
        string WriteCompteRendu();
        void SetupSource(string source);
    }
}
