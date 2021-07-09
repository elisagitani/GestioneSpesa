namespace GestioneSpesa
{
    public class ManagerHandler : AbstractHandler
    {
        public override string Handle(Spesa request)
        {
            if (request.Importo <=400)
                return "Manager";
            else
                return base.Handle(request);
        }
    }
}