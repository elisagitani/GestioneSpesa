using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpesa.Factory
{
    public class ImportoViaggio : IImportoRimborsato
    {
        public decimal CalcolaImportoRimborsato(Spesa s)
        {
            return s.Importo + 50;
        }
    }
}
