namespace GestioneSpesa
{
    public class CeoHandler: AbstractHandler
    {
        public override string Handle(Spesa request)
        {
            if (request.Importo > 1000 && request.Importo<=2500)
                return "CEO";
            else
                return base.Handle(request);
        }
    }
}