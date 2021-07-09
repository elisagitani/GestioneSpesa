using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpesa
{
    public class Spesa
    {
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
        public string Descrizione { get; set; }
        public decimal Importo { get; set; }
        public bool Approvata { get; set; }
        public string LvlApprov { get; set; }
        public decimal ImportoRimborsato { get; set; }


        public string GetInfo()
        {
            return $"Data: {Data.ToShortDateString()} \nCategoria: {Categoria} \nDescrizione: {Descrizione} \nImporto: {Importo} euro";
        }

        public string GetInfoApprovazione()
        {
            if (Approvata)
                return $"{Data.ToShortDateString()};{Categoria};{Descrizione};APPROVATA;{LvlApprov};{ImportoRimborsato}";
            else
                return $"{Data.ToShortDateString()};{Categoria};{Descrizione};RESPINTA;-;-";
        }
    }
}
