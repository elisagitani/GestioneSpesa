namespace GestioneSpesa
{
    public class OperationalManagerHandler: AbstractHandler
    {
        public override string Handle(Spesa request)
        {
            if (request.Importo > 400 && request.Importo<=1000)
                return "Operational Manager";
            else
                return base.Handle(request);
        }
    }
}