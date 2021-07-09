using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpesa.Factory
{
    public interface IImportoRimborsato
    {
        decimal CalcolaImportoRimborsato(Spesa s);
    }
}
