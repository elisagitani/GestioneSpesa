using GestioneSpesa.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestioneSpesa
{
    class Program
    {
        public static List<Spesa> listaSpesa = new();
        public static IEnumerable<Spesa> spesaApprovata;
        public static IEnumerable<Spesa> spesaNonApprovata;
        static void Main(string[] args)
        {
            Console.WriteLine("================Benvenuto nell'app Gestione spesa==================");

            #region FileSystemWatcher

            FileSystemWatcher fsw = new FileSystemWatcher();
            fsw.Path = @"C:\Users\elisa.gitani\Desktop\GestioneSpesa";
            fsw.Filter = "*.txt";
            fsw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fsw.EnableRaisingEvents = true;

            fsw.Created += Fsw_Created;

            Console.WriteLine("Premi q per uscire");
            while (Console.Read() != 'q') ;
            #endregion

            #region ChainOfResponsibility

            var manager = new ManagerHandler();
            var operationalManager = new OperationalManagerHandler();
            var ceo = new CeoHandler();

            manager.SetNext(operationalManager).SetNext(ceo);       //creazione della chain

            foreach(var item in listaSpesa)
            {
                Console.WriteLine($"\nDettagli spesa: \n\n{item.GetInfo()}");
                Console.WriteLine("\nChi se ne occupa?");

                var result = manager.Handle(item);

                if (result != null)
                {
                    Console.WriteLine($"Livello di approvazione: {result}");
                    item.LvlApprov = result;
                    item.Approvata = true;
                }
                else
                {
                    Console.WriteLine($"Spesa non approvata!!! Il suo importo è maggiore di 2500 euro");
                    item.LvlApprov = null;
                    item.Approvata = false;
                }
            }










            #endregion

            #region Factory

            spesaApprovata = listaSpesa.Where(item => item.Approvata == true);
            spesaNonApprovata = listaSpesa.Where(item => item.Approvata == false);
            foreach(var item in spesaApprovata)
            {
                string categoria = item.Categoria;
                IImportoRimborsato tipoRimborso;

                tipoRimborso = ImportoFactory.GetImporto(categoria);

                item.ImportoRimborsato=tipoRimborso.CalcolaImportoRimborsato(item);

                //Console.WriteLine(item.ImportoRimborsato);
            }

            //foreach(var item in spesaApprovata)
            //{
            //    Console.WriteLine(item.GetInfoApprovazione());
            //}
            //foreach (var item in spesaNonApprovata)
            //{
            //    Console.WriteLine(item.GetInfoApprovazione());
            //}

            #endregion

            #region SalvataggioSuFile

            try
            {
                if (!Directory.Exists(@"C:\Users\elisa.gitani\Desktop\NuovaCartella"))
                {
                    Directory.CreateDirectory(@"C:\Users\elisa.gitani\Desktop\NuovaCartella");
                }

                StreamWriter writer = File.CreateText(@"C:\Users\elisa.gitani\Desktop\NuovaCartella\spese_elaborate.txt");

                foreach(var item in spesaApprovata)
                {
                    writer.WriteLine(item.GetInfoApprovazione());
                }
                foreach (var item in spesaNonApprovata)
                {
                    writer.WriteLine(item.GetInfoApprovazione());
                }
                writer.Flush();
                writer.Close();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($" I/O Error: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Errore Generico: {ex.Message}");
            }
            #endregion
        }

        private static void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"\nFile {e.Name} creato con successo");
            Console.WriteLine("\n=====================================================================");
            Spesa s = new Spesa();
            StreamReader reader = File.OpenText(e.FullPath);
            string row;

            while ((row = reader.ReadLine()) != null)
            {
                Console.WriteLine(row);
                string[] parti=SpacchettaStringa(row);
                listaSpesa = GeneraLista(parti,listaSpesa);

            }

            Console.WriteLine("======================================================================");
        }

        private static List<Spesa> GeneraLista(string[] parti, List<Spesa> spesa)
        {
            Spesa s = new();
          
            s.Data = DateTime.Parse(parti[0]);
            s.Categoria = parti[1];
            s.Descrizione = parti[2];
            s.Importo =decimal.Parse(parti[3]);
            spesa.Add(s);
            return spesa;
        }

        private static string[] SpacchettaStringa(string row)
        {
            
            string[] parti=row.Split(';');
            return parti;
        }
    }

  
    
}
